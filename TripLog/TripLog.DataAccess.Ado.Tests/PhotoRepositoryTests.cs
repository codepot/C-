using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripLog.Domain;

namespace TripLog.DataAccess.Ado.Tests
{
    [TestClass]
    public class PhotoRepositoryTests
    {
        private const string TestName = "TestName";
        private const string TestDescription = "TestDescrption";
        private const string TestLocation = "TestLocation";
        private const string TestPhotoFormat = "jpg";
        private const string TestExportFileName = "TestExportFileName";
        private const string TestInternalFileName = "TestInternalFileName";
        private double? TestLatitude = +33.333333;
        private double? TestLongitude = 120.120120;
        private DateTime? TestPhotoDate = new DateTime(2015, 1, 1);

        /// <summary>
        /// Deletes any left-over test records.
        /// </summary>
        /// <remarks>
        /// This is kind of a "cheating" way to do integration testing - using
        /// a "real" database without starting in a known state. A better
        /// set of integration tests would involve mocking a database, which
        /// is a bit outside the scope of this course.
        /// </remarks>
        [TestCleanup]
        public void Cleanup()
        {
            var _repo = new PhotoRepository();
            var list = _repo.Fetch(null).ToList();

            var toDelete = list.Where(o => o.Name == TestName);
            foreach (var item in toDelete)
            {
                item.IsMarkedForDeletion = true;
                _repo.Persist(item);
            }
        }

        [TestMethod]
        public void PhotoRepository_FetchAll_ReturnsData()
        {
            // Arrange
            var repo = new PhotoRepository();

            // Act
            var list = repo.Fetch();

            // Assert
            Assert.IsNotNull(list);
            Console.WriteLine(list.Count().ToString() + "bb>>>");
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void PhotoRepository_FetchOne_ReturnsOne()
        {
            // Arrange
            var repo = new PhotoRepository();
            var all = repo.Fetch(null).ToList();
            var id = all[0].PhotoId;
            var name = all[0].Name;

            var item = repo.Fetch(id).Single();

            Assert.IsNotNull(item);
            Assert.IsTrue(item.PhotoId == id);
            Assert.IsTrue(item.Name == name);
            Assert.IsFalse(item.IsMarkedForDeletion);
            Assert.IsFalse(item.IsDirty);
        }

        [TestMethod]
        public void PhotoRepository_InsertDelete()
        {
            
            // Arrange
            var repo = new PhotoRepository();
            var newItem = new Photo
            {
                Name = TestName,
                Description = TestDescription,
                Location = TestLocation,
                PhotoFormat = TestPhotoFormat,
                ExportFileName = TestExportFileName,
                InternalFileName = TestInternalFileName,
                Latitude = TestLatitude,
                Longitude =  TestLongitude,
                PhotoDate = TestPhotoDate
            };

            // Act for Insert
            var item = repo.Persist(newItem);
            var newId = item.PhotoId;

            // Assert for Insert - Make sure local object is updated
            Assert.IsTrue(item.PhotoId > 0);
            Assert.IsFalse(item.IsMarkedForDeletion);
            Assert.IsFalse(item.IsDirty);

            // Assert for Insert - Make sure refetched object is correct
            var refetch = repo.Fetch(newId).First();

            // Verify all properties were correctly saved and retrieved
            Assert.IsTrue(refetch.PhotoId == newId);
            Assert.IsTrue(refetch.Name == TestName);
            Assert.IsTrue(refetch.Description == TestDescription);
            Assert.IsTrue(refetch.Location == TestLocation);
            Assert.IsTrue(refetch.PhotoFormat == TestPhotoFormat);
            Assert.IsTrue(refetch.ExportFileName == TestExportFileName);
            Assert.IsTrue(refetch.InternalFileName == TestInternalFileName);
            Assert.IsTrue(refetch.Latitude == TestLatitude);
            Assert.IsTrue(refetch.Longitude ==  TestLongitude);
            Assert.IsTrue(refetch.PhotoDate == TestPhotoDate);

            // Verify change tracking properties set correctly after insert
            Assert.IsFalse(refetch.IsMarkedForDeletion);
            Assert.IsFalse(refetch.IsDirty);

            // Clean-up (Act for Delete)
            item.IsMarkedForDeletion = true;
            repo.Persist(item);

            // Assert for Delete
            var result = repo.Fetch(newId);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void PhotoRepository_InsertUpdateDelete()
        {
            // Arrange
            var repo = new PhotoRepository();
            var newItem = new Photo
            {
                Name = TestName,
                Description = TestDescription,
                Location = TestLocation,
                PhotoFormat = TestPhotoFormat,
                ExportFileName = TestExportFileName,
                InternalFileName = TestInternalFileName,
                Latitude = TestLatitude,
                Longitude = TestLongitude,
                PhotoDate = TestPhotoDate
            };
            var item = repo.Persist(newItem);
            var newId = item.PhotoId;

            // Act for Update
            item.Name = TestName + "x";
            item.Description = TestDescription + "x";
            item.Location = TestLocation + "x";
            item.PhotoFormat = TestPhotoFormat + "x";
            item.ExportFileName = TestExportFileName + "x";
            item.InternalFileName = TestInternalFileName + "x";
            item.Latitude = TestLatitude + 1.0;
            item.Longitude = TestLongitude + 1.0;
            item.PhotoDate = TestPhotoDate.Value.AddDays(1);

            // For now, we need to explicitly mark the item as
            // Dirty - we'll enhance our domain model to automatically
            // set the IsDirty property when any property changes
            // a bit later.
            item.IsDirty = true;

            var updatedItem = repo.Persist(item);

            // Check properties of local object after saving
            Assert.IsTrue(updatedItem.IsDirty == false);
            Assert.IsTrue(updatedItem.IsMarkedForDeletion == false);

            Assert.IsTrue(updatedItem.Name == TestName + "x");
            Assert.IsTrue(updatedItem.Description == TestDescription + "x");
            Assert.IsTrue(updatedItem.Location == TestLocation + "x");
            Assert.IsTrue(updatedItem.PhotoFormat == TestPhotoFormat + "x");
            Assert.IsTrue(updatedItem.ExportFileName == TestExportFileName + "x");
            Assert.IsTrue(updatedItem.InternalFileName == TestInternalFileName + "x");
            Assert.IsTrue(updatedItem.Latitude == TestLatitude + 1.0);
            Assert.IsTrue(updatedItem.Longitude == TestLongitude + 1.0);
            Assert.IsTrue(updatedItem.PhotoDate == TestPhotoDate.Value.AddDays(1));

            // Assert for Update
            var refetch = repo.Fetch(newId).First();

            // Check properties of refetched domain object to make
            // sure they were saved and re-fetched.
            Assert.IsTrue(refetch.IsDirty == false);
            Assert.IsTrue(refetch.IsMarkedForDeletion == false);

            /*

            Assert.IsTrue(refetch.Name == TestName + "x");
            Assert.IsTrue(refetch.Description == TestDescription + "x");
            Assert.IsTrue(refetch.Location == TestLocation + "x");
            Assert.IsTrue(refetch.PhotoFormat == TestPhotoFormat + "x");
            Assert.IsTrue(refetch.ExportFileName == TestExportFileName + "x");
            Assert.IsTrue(refetch.InternalFileName == TestInternalFileName + "x");
            Assert.IsTrue(refetch.Latitude == TestLatitude + 1.0);
            Assert.IsTrue(refetch.Longitude == TestLongitude + 1.0);
            Assert.IsTrue(refetch.PhotoDate == TestPhotoDate.Value.AddDays(1));

            // Clean-up (Act for Delete)
            item.IsMarkedForDeletion = true;
            repo.Persist(item);

            // Assert for Delete
            var result = repo.Fetch(newId);
            Assert.IsFalse(result.Any());

    */
        }
    }
}
