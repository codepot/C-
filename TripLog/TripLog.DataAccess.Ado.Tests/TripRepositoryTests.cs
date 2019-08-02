using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripLog.Domain;
using TripLog.DataAccess.Ado;

namespace TripLog.DataAccess.Ado.Tests
{
    [TestClass]
    public class TripRepositoryTests

    {
        #region Fields

        TripRepository _repo;

        private const string TestTripName = "TestTrip";
        private const string TestTripDescription = "TestTripDescription";
        private DateTime TestTripStartDate = new DateTime(2012, 10, 1);
        private DateTime TestTripEndDate = new DateTime(2012, 10, 23);


        #endregion

        #region Initialize and Cleanup

        [TestInitialize]
        public void Initialize()
        {
            _repo = new TripRepository();
        }

        /// <summary>
        /// Deletes any left-over test records.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            /*
            var _TripRepo = new TripRepository();
            var list = _TripRepo.Fetch(null).ToList();

            var toDelete = list.Where(o => o.Name == TestTripName );
            foreach (var item in toDelete)
            {
                item.IsMarkedForDeletion = true;
                _TripRepo.Persist(item);
            }
            */
        }

        private Trip CreateSampleTrip()
        {
            Trip trip = new Trip();
            trip.Name = TestTripName;
            trip.Description = TestTripDescription;
            trip.StartDate = TestTripStartDate;
            trip.EndDate = TestTripEndDate; 

            // Add Two child Waypoints
            var waypoint1 = new Waypoint
            {
                Name = "WaypointA",
                Description = "WaypointADescription",
                Street = "WaypointAStreet",
                City = "WaypointACity",
                State = "WaypointAState",
                Country = "WaypointACountry",
                Latitude = -5.5,
                Longitude = -13.5
            };
            trip.Waypoints.Add(waypoint1);
            
            var waypoint2 = new Waypoint
            {
                Name = "WaypointB",
                Description = "WaypointBDescription",
                Street = "WaypointBStreet",
                City = "WaypointBCity",
                State = "WaypointBState",
                Country = "WaypointBCountry",
                Latitude = -5.0,
                Longitude = -13.0
            };
            trip.Waypoints.Add(waypoint2);

            // Add two child Photos
            Photo photoA = new Photo
            {
                Name = "PhotoAname",
                Description = "PhotoADescription",
                Location = "PhotoALocation",
                PhotoFormat = "FmtA",
                ExportFileName = "PhotoAExportFileName",
                InternalFileName = "PhotoAImportFileName",
                Thumbnail = null,
                Latitude = 89.0,
                Longitude = -179.894,
                PhotoDate = new DateTime(2020, 1, 1)
            };
            trip.Photos.Add(photoA);

            var photoB = new Photo
            {
                Name = "PhotoBname",
                Description = "PhotoBDescription",
                Location = "PhotoBLocation",
                PhotoFormat = "FmtB",
                ExportFileName = "PhotoBExportFileName",
                InternalFileName = "PhotoBImportFileName",
                Thumbnail = null,
                Latitude = 89.123456789,
                Longitude = 179.894456,
                PhotoDate = new DateTime(2020, 1, 2)
            };
            trip.Photos.Add(photoB);

            return trip;
        }

        #endregion

        #region Fetch Tests

        [TestMethod]
        public void TripRepository_FetchNull_ReturnsAll()
        {
            // Arrange

            // Act
            var list = _repo.Fetch();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void TripRepository_FetchOne_ReturnsOne()
        {
            // Arrange
            var all = _repo.Fetch().ToList();
            var tripId = all[0].TripId;
            var name = all[0].Name;

            var item = _repo.Fetch(tripId).Single();

            Assert.IsNotNull(item);
            Assert.IsTrue(item.TripId == tripId);
            Assert.IsTrue(item.Name == name);
            Assert.IsFalse(item.IsMarkedForDeletion);
            Assert.IsFalse(item.IsDirty);
        }

        [TestMethod]
        public void TripRepository_FetchNonExistent_ReturnsEmptyList()
        {
            // Arrange

            // Try to fetch non-existent Trip
            var resultList = _repo.Fetch(-99);

            Assert.IsNotNull(resultList);
        }

        #endregion

        //#region Persist Tests

        [TestMethod]
        public void TripRepository_InsertDelete()
        {
            // Arrange
            Trip newTrip = CreateSampleTrip();

            // Act for Insert
            var existingTrip = _repo.Persist(newTrip);
            var testTripId = existingTrip.TripId;
            Assert.IsTrue(existingTrip.TripId > 0);
            /*
            // Assert for Insert - Make sure local object is updated
            // with non-zero Id values
            Assert.IsTrue(existingTrip.TripId > 0);
            Assert.IsFalse(existingTrip.IsMarkedForDeletion);
            Assert.IsFalse(existingTrip.IsDirty);
            Assert.IsTrue(existingTrip.Waypoints[0].WaypointId > 0);
            Assert.IsTrue(existingTrip.Waypoints[1].WaypointId > 0);
            Assert.IsTrue(existingTrip.Photos[0].PhotoId > 0);
            Assert.IsTrue(existingTrip.Photos[1].PhotoId > 0);

            // re-fetching creates a different C# Trip instance
            var refetch = _repo.Fetch(testTripId).First();

            // Assert for Insert - Make sure local object is updated
            Assert.IsTrue(refetch.TripId > 0);
            Assert.IsFalse(refetch.IsMarkedForDeletion);
            Assert.IsFalse(refetch.IsDirty);
            Assert.IsTrue(refetch.Waypoints[0].WaypointId > 0);
            Assert.IsTrue(refetch.Waypoints[1].WaypointId > 0);
            Assert.IsTrue(refetch.Photos[0].PhotoId > 0);
            Assert.IsTrue(refetch.Photos[1].PhotoId > 0);

            // Assert for Insert - Make sure refetched object is correct
            Assert.IsTrue(refetch.TripId == testTripId);
            Assert.IsFalse(refetch.IsMarkedForDeletion);
            Assert.IsFalse(refetch.IsDirty);
            Assert.IsTrue(refetch.Name == TestTripName);
            Assert.IsTrue(refetch.Description == TestTripDescription);
            Assert.IsTrue(refetch.StartDate == TestTripStartDate);
            Assert.IsTrue(refetch.EndDate == TestTripEndDate);
            Assert.IsTrue(refetch.Waypoints.Count() == 2);
            Assert.IsTrue(refetch.Waypoints[0].WaypointId > 0);
            Assert.IsTrue(refetch.Waypoints[1].WaypointId > 0);
            Assert.IsTrue(refetch.Photos.Count() == 2);
            Assert.IsTrue(refetch.Photos[0].PhotoId > 0);
            Assert.IsTrue(refetch.Photos[1].PhotoId > 0);

            
            // Clean-up (Act for Delete)
            existingTrip.IsMarkedForDeletion = true;
            _repo.Persist(existingTrip);

            // Assert for Delete
            var result = _repo.Fetch(testTripId);
            Assert.IsFalse(result.Any());
            */
        }
       
        [TestMethod]
        public void TripRepository_InsertUpdateDelete()
        {
            /*
           // Arrange
           Trip newTrip = CreateSampleTrip();

           // Act for Insert
           var existingTrip = _repo.Persist(newTrip);

           // Act for Update
           // Modify each scalar property
           existingTrip.Name = "UpdatedTripName";
           existingTrip.Description = "UpdatedTripDescription";
           existingTrip.StartDate = new DateTime(2011, 2, 4);
           existingTrip.EndDate = new DateTime(2011, 3, 4);
           existingTrip.IsDirty = true;

           //// Delete a Waypoint
           //var TripGenreForDeletion = existingTrip.TripGenres.Where(o => o.GenreId == _genres[0].GenreId).FirstOrDefault();
           //TripGenreForDeletion.IsMarkedForDeletion = true;
           //var TripGenreDeletedGenreId = TripGenreForDeletion.GenreId;

           //// Leave one TripGenre unchanged
           //var TripGenreUnchangedGenreid = existingTrip.TripGenres[1].GenreId;

           //// Insert a TripGenre
           //var TripGenreInsertedGenreId = _genres[2].GenreId;
           //existingTrip.TripGenres.Add(new TripGenre { GenreId = TripGenreInsertedGenreId });

           //// Update a Credits member credit
           //var credit0 = existingTrip.Credits.Where(o => o.PersonId == creditPersonId0).First();
           //credit0.Character = "George";
           //// Need to set dirty flag on any modified child objects
           //credit0.IsDirty = true;

           //// Delete a Credits member
           //var credit1 = existingTrip.Credits.Where(o => o.PersonId == creditPersonId1).First();
           //credit1.IsMarkedForDeletion = true;

           //// Insert a Credits member
           //var creditPersonId2 = _people[2].PersonId;
           //Credit credit2 = new Credit
           //{
           //    PersonId = creditPersonId2,
           //    CreditTypeId = _creditTypes[1].CreditTypeId,
           //    Character = "Samantha"
           //};
           //existingTrip.Credits.Add(credit2);

           // Perform the update and refetch again
           // updatedItem and testTrip refer to the same C# object
           existingTrip = _repo.Persist(existingTrip);

           // Make sure the Trip object changes are reflected in the
           // Trip object that remains on the client.
           Assert.IsNotNull(existingTrip);
           Assert.IsTrue(existingTrip.IsDirty == false);
           Assert.IsTrue(existingTrip.IsMarkedForDeletion == false);
           Assert.IsTrue(existingTrip.Name == "UpdatedTripName");
           Assert.IsTrue(existingTrip.Description == "UpdatedTripDescription");
           Assert.IsTrue(existingTrip.StartDate == new DateTime(2011, 2, 4));
           Assert.IsTrue(existingTrip.EndDate == new DateTime(2011, 3, 4));

           //Assert.IsTrue(existingTrip.TripGenres.Count() == 2);

           //var changedTripGenre0 = existingTrip.TripGenres
           //    .Where(o => o.GenreId == TripGenreUnchangedGenreid)
           //    .FirstOrDefault();
           //Assert.IsNotNull(changedTripGenre0);
           //Assert.IsFalse(changedTripGenre0.IsDirty);

           //var changedTripGenre1 = existingTrip.TripGenres
           //    .Where(o => o.GenreId == TripGenreInsertedGenreId)
           //    .FirstOrDefault();
           //Assert.IsNotNull(changedTripGenre1);
           //Assert.IsFalse(changedTripGenre1.IsDirty);

           //var changedTripGenre2 = existingTrip.TripGenres
           //    .Where(o => o.GenreId == TripGenreDeletedGenreId)
           //    .FirstOrDefault();
           //Assert.IsNull(changedTripGenre2);

           //Assert.IsTrue(existingTrip.Credits.Count() == 2);

           //var changedCredit0 = existingTrip.Credits
           //    .Where(o => o.PersonId == creditPersonId0).FirstOrDefault();
           //Assert.IsNotNull(changedCredit0);
           //Assert.IsTrue(changedCredit0.Character == "George");
           //Assert.IsFalse(changedCredit0.IsDirty);

           //var changedCredit1 = existingTrip.Credits
           //    .Where(o => o.PersonId == creditPersonId1).FirstOrDefault();
           //Assert.IsNull(changedCredit1);

           //var changedCredit2 = existingTrip.Credits
           //    .Where(o => o.PersonId == creditPersonId2).FirstOrDefault();
           //Assert.IsNotNull(changedCredit2);
           //Assert.IsFalse(changedCredit2.IsDirty);


           // re-fetching the same Trip from the database creates a
           // new C# object, that should be an exact replica of testTrip
           var refetch = _repo.Fetch(existingTrip.TripId).FirstOrDefault();

           // Assert - Make sure updated item has proper flags set
           Assert.IsTrue(existingTrip.IsDirty == false);
           Assert.IsTrue(existingTrip.IsMarkedForDeletion == false);

           // Assert - Make sure re-fetched Trip has expected properties
           Assert.IsNotNull(refetch);
           Assert.IsTrue(refetch.IsDirty == false);
           Assert.IsTrue(refetch.IsMarkedForDeletion == false);
           Assert.IsTrue(refetch.Name == "UpdatedTripName");
           Assert.IsTrue(refetch.Description == "UpdatedTripDescription");
           Assert.IsTrue(refetch.StartDate == new DateTime(2011, 2, 4));
           Assert.IsTrue(refetch.EndDate == new DateTime(2011, 3, 4));

           //Assert.IsTrue(refetch.TripGenres.Count() == 2);
           //Assert.IsTrue(refetch.TripGenres
           //    .Where(o => o.GenreId == TripGenreUnchangedGenreid).Count() == 1);
           //Assert.IsTrue(refetch.TripGenres
           //    .Where(o => o.GenreId == TripGenreInsertedGenreId).Count() == 1);
           //Assert.IsTrue(refetch.TripGenres
           //    .Where(o => o.GenreId == TripGenreDeletedGenreId).Count() == 0);
           //Assert.IsTrue(refetch.Credits[0].Character == "George");

           //Assert.IsTrue(refetch.Credits.Count() == 2);
           //var refetchCredit0 = refetch.Credits
           //    .Where(o => o.PersonId == creditPersonId0).FirstOrDefault();
           //var refetchCredit1 = refetch.Credits
           //    .Where(o => o.PersonId == creditPersonId1).FirstOrDefault();
           //var refetchCredit2 = refetch.Credits
           //    .Where(o => o.PersonId == creditPersonId2).FirstOrDefault();
           //Assert.IsTrue(refetchCredit0.Character == "George");
           //Assert.IsNull(refetchCredit1);
           //Assert.IsNotNull(refetchCredit2);

           // Clean-up (Act for Delete)
           existingTrip.IsMarkedForDeletion = true;
           _repo.Persist(existingTrip);

           // Assert for Delete
           var result = _repo.Fetch(existingTrip.TripId);
           Assert.IsFalse(result.Any());
       }

       //#endregion

       //#region IsGraphDirty Test

       //[TestMethod]
       //public void TripRepository_CreditDirty_SetsGraphDirty()
       //{
       //    // Arrange
       //    var repo = new TripRepository();
       //    var all = repo.Fetch(null).ToList();
       //    var TripId = all[0].TripId;
       //    var title = all[0].Title;

       //    var item = repo.Fetch(TripId).Single();

       //    // Add one TripGenre to change a leaf
       //    // of the object graph
       //    var sg = new TripGenre();
       //    var genreRepository = new GenreRepository();
       //    var g = genreRepository.Fetch().First();
       //    sg.GenreId = g.GenreId;
       //    item.TripGenres.Add(sg);

       //    Assert.IsNotNull(item);
       //    Assert.IsTrue(item.TripId == TripId);
       //    Assert.IsTrue(item.Title == title);
       //    Assert.IsFalse(item.IsMarkedForDeletion);

       //    // The IsDirty flag should be false
       //    Assert.IsFalse(item.IsDirty);

       //    // The IsGraphChanges property should
       //    // be true, indicating the change to TripGenres
       //    Assert.IsTrue(item.IsGraphDirty);
       //}

       //#endregion

       //#region Transaction Test

       //[TestMethod]
       //public void TripRepository_InvalidCredit_TransactionRollsBack()
       //{
       //    // Arrange
       //    Trip newTrip = CreateSampleTrip();

       //    // Act - Insert Trip with a bad Cast member record
       //    // (Doesn't refer to an existing person id).
       //    newTrip.Credits[0].PersonId = -1;
       //    try
       //    {
       //        // Should throw exception
       //        var existingTrip = _repo.Persist(newTrip);
       //        Assert.Fail();
       //    }
       //    catch (Exception ex)
       //    {
       //        Assert.IsInstanceOfType(ex, typeof(SqlException));
       //    }

       //    // Make sure parent Trip object was NOT saved.
       //    var savedTrip = _repo.Fetch()
       //        .Where(o => o.Title == "TestTitle")
       //        .FirstOrDefault();
       //    Assert.IsNull(savedTrip);
       //
    */
        }

        //#endregion
    }
}
