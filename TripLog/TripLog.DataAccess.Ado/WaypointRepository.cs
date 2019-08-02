using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TripLog.Domain;
using Ucla.Common.ExtensionMethods;

namespace TripLog.DataAccess.Ado
{
    public class WaypointRepository
    {

        #region PersistChild

        public static Waypoint Persist(Waypoint Waypoint, SqlConnection conn)
        {

            if (Waypoint.WaypointId == 0 && Waypoint.IsMarkedForDeletion)
            {
                Waypoint = null;
            }
            else if (Waypoint.IsMarkedForDeletion)
            {
                DeleteEntity(Waypoint, conn);
                Waypoint = null;
            }
            else if (Waypoint.WaypointId == 0)
            {
                InsertEntity(Waypoint, conn);
                Waypoint.IsDirty = false;
            }
            else if (Waypoint.IsDirty)
            {
                UpdateEntity(Waypoint, conn);
                Waypoint.IsDirty = false;
            }
            return Waypoint;

        }

        #endregion

        #region SQL

        internal static void InsertEntity(Waypoint item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                var sql = new StringBuilder();
                sql.Append("insert Waypoint (Name, StartDate, EndDate, Description, Street, City, State, Country, Latitude, Longitude, TripId)");
                sql.Append("values ( @Name, @StartDate, @EndDate, @Description, @Street, @City, @State, @Country, @Latitude, @Longitude, @TripId);");
                sql.Append("select cast ( scope_identity() as int);");
                cmd.CommandText = sql.ToString();

                SetCommonParameters(item, cmd);

                item.WaypointId = (int)cmd.ExecuteScalar();
            }
        }

        public Waypoint Persist(Waypoint waypoint)
        {
            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                InsertEntity(waypoint, conn);
                waypoint.IsDirty = false;
            }
            return waypoint;
        }

        internal static void UpdateEntity(Waypoint item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                var sql = new StringBuilder();
                sql.Append("update Waypoint set ");
                sql.Append(" Name = @Name, ");
                sql.Append(" StartDate = @StartDate, ");
                sql.Append(" EndDate = @EndDate, ");
                sql.Append(" Description = @Description, ");
                sql.Append(" Street = @Street, ");
                sql.Append(" City = @City, ");
                sql.Append(" State = @State, ");
                sql.Append(" Country = @Country, ");
                sql.Append(" Latitude = @Latitude, ");
                sql.Append(" Longitude = @Longitude, ");
                sql.Append(" TripId = @TripId ");
                sql.Append("where WaypointId = @WaypointId ");
                cmd.CommandText = sql.ToString();

                SetCommonParameters(item, cmd);
                cmd.Parameters.AddWithValue("@WaypointId", item.WaypointId);

                cmd.ExecuteNonQuery();
            }
        }

        public Waypoint UpdateWaypoint(Waypoint waypoint)
        {
            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                UpdateEntity(waypoint, conn);               
            }
            return waypoint;
        }


        public void DeleteWaypoint(Waypoint waypoint)
        {
            waypoint.IsMarkedForDeletion = true;
            var connString = ConfigurationManager
                .ConnectionStrings["AppConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                DeleteEntity(waypoint, conn);
            }           
        }

        internal static void DeleteEntity(Waypoint item, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete Waypoint where WaypointId = @WaypointId";
                cmd.Parameters.AddWithValue("@WaypointId", item.WaypointId);
                cmd.ExecuteNonQuery();
            }
        }


        public IEnumerable<Waypoint> Fetch(object criteria = null)
        {
            var data = new List<Waypoint>();
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
                        cmd.CommandText = "select * from Waypoint";
                    }
                    else if (criteria is int)
                    {
                        cmd.CommandText = "select * from Waypoint where WaypointId = @WaypointId";
                        cmd.Parameters.AddWithValue("@WaypointId", (int)criteria);
                    }
                    else
                    {
                        var msg = String.Format(
                            "WaypointRepository: Unknown criteria type: {0}",
                            criteria);
                        throw new InvalidOperationException(msg);
                    }
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var p = new Waypoint();
                        p.WaypointId = dr.AsInt32("WaypointId");                      
                        p.Name = dr.AsString("Name");
                        p.Description = dr.AsString("Description");
                        p.StartDate = dr.AsDateTime("StartDate");
                        p.EndDate = dr.AsDateTime("EndDate");
                        p.Street = dr.AsString("Street");
                        p.City = dr.AsString("City");
                        p.State = dr.AsString("State");
                        p.Country = dr.AsString("Country");
                        p.Latitude = dr.AsNullableDouble("Latitude");
                        p.Longitude = dr.AsNullableDouble("Longitude");
                        p.TripId = dr.AsInt32("TripId");
                        // p.WaypointId = dr.AsNullableInt32("WaypointId");                   

                        p.IsDirty = false;
                        data.Add(p);
                    }
                }
            }
            return data;
        }

        private static void SetCommonParameters(Waypoint item, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Name", item.Name);
            cmd.Parameters.AddWithValue("@StartDate", item.StartDate.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@EndDate", item.EndDate.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@Description", item.Description);
            cmd.Parameters.AddWithValue("@Street", item.Street);
            cmd.Parameters.AddWithValue("@City", item.City);
            cmd.Parameters.AddWithValue("@State", item.State);
            cmd.Parameters.AddWithValue("@Country", item.Country);
            cmd.Parameters.AddWithValue("@Latitude", item.Latitude.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@Longitude", item.Longitude.AsSqlParameterValue());
            cmd.Parameters.AddWithValue("@TripId", item.TripId);
        }

        #endregion

    }
}
