using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TripLog.Domain;
using Ucla.Common.Interfaces;
using Ucla.Common.ExtensionMethods;

namespace TripLog.DataAccess.Ado
{
    public class TripRepository : IRepository<Trip>
    {
        #region IRepository<Trip> Members

        public IEnumerable<Trip> Fetch(object criteria = null)
        {
            var data = new List<Trip>();
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
                        var sql = new StringBuilder();
                        sql.Append("select * from Trip; ");
                        sql.Append("select * from Waypoint order by Waypoint.StartDate; ");
                        sql.Append("select * from Photo where TripId is not null; ");
                        cmd.CommandText = sql.ToString();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var t = new Trip();
                            t.TripId = dr.AsInt32("TripId");
                            t.Name = dr.AsString("Name");
                            t.Description = dr.AsString("Description");
                            t.StartDate = dr.AsDateTime("StartDate");
                            t.EndDate = dr.AsDateTime("EndDate");
                            t.IsDirty = false;
                            data.Add(t);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var w = new Waypoint();
                            w.WaypointId = dr.AsInt32("WaypointId");
                            w.TripId = dr.AsInt32("TripId");
                            w.Name = dr.AsString("Name");
                            w.StartDate = dr.AsNullableDateTime("StartDate");
                            w.EndDate = dr.AsNullableDateTime("EndDate");
                            w.Description = dr.AsString("Description");
                            w.Street = dr.AsString("Street");
                            w.State = dr.AsString("State");
                            w.Country = dr.AsString("Country");
                            w.Latitude = dr.AsNullableDouble("Latitude");
                            w.Longitude = dr.AsNullableDouble("Longitude");

                            w.IsDirty = false;
                            data.Where(o => o.TripId == w.TripId).Single().Waypoints.Add(w);
                        }
                        dr.NextResult();
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
                            data.Where(o => o.TripId == p.TripId).Single().Photos.Add(p);
                        }
                    }
                    else if (criteria is int)
                    {
                        var sql = new StringBuilder();
                        sql.Append("select * from Trip where TripId = @TripId; \r\n");
                        sql.Append("select * from Waypoint where TripId = @TripId order by Waypoint.StartDate; \r\n");
                        sql.Append("select * from Photo where TripId = @TripId; ");
                        cmd.CommandText = sql.ToString();
                        cmd.Parameters.AddWithValue("@TripId", (int)criteria);
                        var dr = cmd.ExecuteReader();
                        var t = new Trip();
                        while (dr.Read())
                        {
                            t.TripId = dr.AsInt32("TripId");
                            t.Name = dr.AsString("Name");
                            t.Description = dr.AsString("Description");
                            t.StartDate = dr.AsDateTime("StartDate");
                            t.EndDate = dr.AsDateTime("EndDate");

                            t.IsDirty = false;
                            data.Add(t);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var w = new Waypoint();
                            w.WaypointId = dr.AsInt32("WaypointId");
                            w.TripId = dr.AsInt32("TripId");
                            w.Name = dr.AsString("Name");
                            w.StartDate = dr.AsNullableDateTime("StartDate");
                            w.EndDate = dr.AsNullableDateTime("EndDate");
                            w.Description = dr.AsString("Description");
                            w.Street = dr.AsString("Street");
                            w.State = dr.AsString("State");
                            w.Country = dr.AsString("Country");
                            w.Latitude = dr.AsNullableDouble("Latitude");
                            w.Longitude = dr.AsNullableDouble("Longitude");

                            w.IsDirty = false;
                            t.Waypoints.Add(w);
                        }
                        dr.NextResult();
                        while (dr.Read())
                        {
                            var p = new Photo();
                            p.PhotoId = dr.AsInt32("PhotoId");
                            p.TripId = dr.AsNullableInt32("TripId");
                           
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
                            t.Photos.Add(p);
                        }
                    }
                    else
                    {
                        var msg = String.Format(
                            "TripRepository: Unknown criteria type: {0}",
                            criteria);
                        throw new InvalidOperationException(msg);
                    }
                }
            }
            return data;
        }

        public void DeleteWaypoint(Trip currentTrip)
        {
            currentTrip.IsMarkedForDeletion = true;
            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                DeleteEntity(currentTrip, conn);
            }
        }


        /// <summary>
        /// Saves entity changes to the database
        /// </summary>
        /// <param name="item">object to be saved</param>
        /// <returns>updated entity, or null if the entity is deleted</returns>
        public Trip Persist(Trip item)
        {
            if (item.TripId == 0 && item.IsMarkedForDeletion)
            {
                item = null;
            }

            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;
            using (TransactionScope ts = new TransactionScope())
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    if (item.IsMarkedForDeletion)
                    {
                        // Also Deletes Children
                        DeleteEntity(item, conn);
                        item = null;
                    }
                    else if (item.TripId == 0)
                    {
                        InsertEntity(item, conn);
                        PersistChildren(item, conn);
                        item.IsDirty = false;
                    }
                    else if (item.IsDirty)
                    {
                        UpdateEntity(item, conn);
                        PersistChildren(item, conn);
                        item.IsDirty = false;
                    }
                    else
                    {
                        // No changes to Trip, but might be changes to children
                        PersistChildren(item, conn);
                    }
                }
                ts.Complete();
            }
            return item;
        }

        private static void PersistChildren(Trip trip, SqlConnection conn)
        {
            foreach (var waypoint in trip.Waypoints)
            {
                waypoint.TripId = trip.TripId;
                WaypointRepository.Persist(waypoint, conn);
            }
            foreach (var photo in trip.Photos)
            {
                photo.TripId = trip.TripId;
                PhotoRepository.Persist(photo, conn);
            }
        }

        #endregion

        #region SQL methods

        internal static void DeleteEntity(Trip item, SqlConnection conn)
        {
            // Cascade delete TripGenres
            foreach (var photo in item.Photos)
            {
                PhotoRepository.DeleteEntity(photo, conn);
            }

            // Cascade delete Waypoints
            foreach (var Waypoint in item.Waypoints)
            {
                WaypointRepository.DeleteEntity(Waypoint, conn);
            }

            // Delete Trip itself
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete Trip where TripId = @TripId";
                cmd.Parameters.AddWithValue("@TripId", item.TripId);
                cmd.ExecuteNonQuery();
            }
        }

        internal static void InsertEntity(Trip item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                var sql = new StringBuilder();
                sql.Append("insert Trip (Name, Description, "
                    + "StartDate, EndDate)");
                sql.Append("values ( @Name, @Description, "
                    + " @StartDate, @EndDate );");
                sql.Append("select cast ( scope_identity() as int);");
                cmd.CommandText = sql.ToString();

                SetCommonParameters(item, cmd);
                item.TripId = (int)cmd.ExecuteScalar();
            }
        }

        public Trip UpdateTrip(Trip trip)
        {
            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                UpdateEntity(trip, conn);
            }
            return trip;
        }

        internal static void UpdateEntity(Trip item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                var sql = new StringBuilder();
                sql.Append("update Trip set ");
                sql.Append(" Name = @Name, ");
                sql.Append(" Description = @Description, ");
                sql.Append(" StartDate = @StartDate, ");
                sql.Append(" EndDate = @EndDate ");
                sql.Append("where TripId = @TripId");
                cmd.CommandText = sql.ToString();

                SetCommonParameters(item, cmd);
                cmd.Parameters.AddWithValue("@TripId", item.TripId);

                cmd.ExecuteNonQuery();
            }
        }

        private static void SetCommonParameters(Trip item, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Name", item.Name);
            cmd.Parameters.AddWithValue("@Description",
                item.Description);
            cmd.Parameters.AddWithValue("@StartDate",
                item.StartDate);
            cmd.Parameters.AddWithValue("@EndDate",
                item.EndDate);
        }


        #endregion
    }
}
