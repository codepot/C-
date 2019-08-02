using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using Ucla.Common.Interfaces;
using Ucla.Common.ExtensionMethods;
using System.Data;
using System.IO;
using TripLog.Domain;


namespace TripLog.DataAccess.Ado
{
    public class PhotoRepository : IRepository<Photo>
    {
        #region IRepository<Photo> Members

        public IEnumerable<Photo> Fetch(object criteria = null)
        {
            var data = new List<Photo>();
            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    if (criteria == null)
                    {
                        cmd.CommandText = "select * from Photo";
                    }
                    else if (criteria is int)
                    {
                        cmd.CommandText = "select * from Photo where PhotoId = @PhotoId";
                        cmd.Parameters.AddWithValue("@PhotoId", (int)criteria);
                    }
                    else
                    {
                        var msg = String.Format(
                            "PhotoRepository: Unknown criteria type: {0}",
                            criteria);
                        throw new InvalidOperationException(msg);
                    }
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var p = new Photo();
                        p.PhotoId = dr.AsInt32("PhotoId");
                        p.TripId = dr.AsNullableInt32("TripId");
                        // p.WaypointId = dr.AsNullableInt32("WaypointId");
                        p.Name = dr.AsString("Name");
                        p.Description = dr.AsString("Description");
                        p.Location = dr.AsString("Location");
                        p.PhotoFormat = dr.AsString("PhotoFormat");
                        p.ExportFileName = dr.AsString("ExportFileName");
                        p.InternalFileName = dr.AsString("InternalFileName");
                        p.Thumbnail = dr.AsByteArray("Thumbnail");
                        p.PhotoBytes = dr.AsByteArray("PhotoBytes");
                        p.Latitude = dr.AsNullableDouble("Latitude");
                        p.Longitude = dr.AsNullableDouble("Longitude");
                        p.PhotoDate = dr.AsNullableDateTime("PhotoDate");
                        p.IsDirty = false;
                        data.Add(p);
                    }
                }
            }
            return data;
        }

        public static Photo Persist(Photo item, SqlConnection conn)
        {
            if (item.PhotoId == 0 && item.IsMarkedForDeletion)
            {
                item = null;
            }
            else if (item.IsMarkedForDeletion)
            {
                DeleteEntity(item, conn);
                item = null;
            }
            else if (item.PhotoId == 0)
            {
                InsertEntity(item, conn);
                item.IsDirty = false;
            }
            else if (item.IsDirty)
            {
                UpdateEntity(item, conn);
                item.IsDirty = false;
            }

            return item;
        }


        public Photo Persist(Photo item)
        {
            if (item.PhotoId == 0 && item.IsMarkedForDeletion)
            {
                item = null;
            }

            var connString = ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                item = Persist(item, conn);
            }

            return item;
        }

        public void UpdatePhoto(Photo item)
        {
            if (item.PhotoId == 0 && item.IsMarkedForDeletion)
            {
                item = null;
            }

            var connString = ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                UpdateEntity(item, conn);
            }
        }

        #endregion

        internal static void InsertEntity(Photo item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                var sql = new StringBuilder();
                sql.Append("insert Photo (TripId, Name, Description, Location, "
                    + "PhotoFormat, ExportFileName, InternalFileName, Thumbnail, PhotoBytes,"
                    + "Latitude, Longitude, PhotoDate)");
                sql.Append("values (@TripId, @Name, @Description, @Location,"
                    + "@PhotoFormat, @ExportFileName, @InternalFileName, @Thumbnail, @PhotoBytes,"
                    + "@Latitude, @Longitude, @PhotoDate); ");
                sql.Append("select cast ( scope_identity() as int);");
                cmd.CommandText = sql.ToString();
                SetCommonParameters(item, cmd);
                item.PhotoId = (int)cmd.ExecuteScalar();

                //SaveToDisk(item);
            }
        }

        private static void SetCommonParameters(Photo item, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TripId", item.TripId.AsSqlParameterValue());
            //cmd.Parameters.AddWithValue("@WaypointId", item.WaypointId.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@Name", item.Name);
            cmd.Parameters.AddWithValue("@Description", item.Description);
            cmd.Parameters.AddWithValue("@Location", item.Location);
            cmd.Parameters.AddWithValue("@PhotoFormat", item.PhotoFormat);
            cmd.Parameters.AddWithValue("@ExportFileName", item.ExportFileName);
            cmd.Parameters.AddWithValue("@InternalFileName", item.InternalFileName);
            cmd.Parameters.AddWithValue("@PhotoBytes", item.PhotoBytes);
            var parm = new SqlParameter("@Thumbnail", SqlDbType.VarBinary);
            parm.Value = item.Thumbnail.AsSqlParameterValue();
            cmd.Parameters.Add(parm);
            cmd.Parameters.AddWithValue("@Latitude", item.Latitude.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@Longitude", item.Longitude.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@PhotoDate", item.PhotoDate.AsSqlParameterValue());
        }

        internal static void UpdateEntity(Photo item, SqlConnection conn)
        {

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                var sql = new StringBuilder();
                sql.Append("update Photo set ");
                sql.Append(" TripId = @TripId, ");
                //sql.Append(" WaypointId = @WaypointId, ");
                sql.Append(" Name = @Name, ");
                sql.Append(" Description = @Description, ");
                sql.Append(" Location = @Location, ");
                sql.Append(" PhotoFormat = @PhotoFormat, ");
                sql.Append(" ExportFileName = @ExportFileName, ");
                sql.Append(" InternalFileName = @InternalFileName, ");
                sql.Append(" Thumbnail = @Thumbnail, ");
                sql.Append(" PhotoBytes = @PhotoBytes, ");
                sql.Append(" Latitude = @Latitude, ");
                sql.Append(" Longitude = @Longitude, ");
                sql.Append(" PhotoDate = @PhotoDate ");
                sql.Append(" where PhotoId = @PhotoId ");
                cmd.CommandText = sql.ToString();
                SetCommonParameters(item, cmd);
                cmd.Parameters.AddWithValue("@PhotoId", item.PhotoId);
                cmd.ExecuteNonQuery();

                //SaveToDisk(item);
            }
        }




        public void DeletePhoto(Photo photo)
        {
            photo.IsMarkedForDeletion = true;
            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                DeleteEntity(photo, conn);
            }
        }

        internal static void DeleteEntity(Photo item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete Photo where PhotoId = @PhotoId";
                cmd.Parameters.AddWithValue("@PhotoId", item.PhotoId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void SaveToDisk(Photo photo)
        {
            if (photo.PhotoBytes == null) return;
            string exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string photoSubfolder = "Photos";
            string fullPath = exePath + "\\" + photoSubfolder + "\\" + photo.InternalFileName + "." + photo.PhotoFormat;
            File.WriteAllBytes(fullPath, photo.PhotoBytes);
        }

        public static void DeleteFromDisk(Photo photo)
        {
            string exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string photoSubfolder = "Photos";
            string fullPath = exePath + "\\" + photoSubfolder + "\\" + photo.InternalFileName + "." + photo.PhotoFormat;

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            System.IO.File.WriteAllBytes(fullPath, photo.PhotoBytes);
        }

        public static void LoadFromDisk(Photo photo)
        {
            string exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string photoSubfolder = "Photos";
            string fullPath = exePath + "\\" + photoSubfolder + "\\" + photo.InternalFileName + "." + photo.PhotoFormat;
            photo.PhotoBytes = File.ReadAllBytes(fullPath);
        }

    }
}



