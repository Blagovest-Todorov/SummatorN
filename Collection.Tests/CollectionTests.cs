using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests
    {

        [Test]
        public void Test_EmptyConstructor()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act 

            // Assert
            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
            //Assert.AreEqual(nums.ToString(), Is.EqualTo("[]"));

        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Arrange
            var nums = new Collection<int>(5);

            //Act


            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[5]"));
            //  Assert.AreEqual(5, nums.Count);
            //Assert.AreEqual(16, nums.Capacity);


        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            var nums = new Collection<int>(5, 10, 15);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15]"));
        }


        [Test]
        public void Test_Collection_Add()
        {
            var nums = new Collection<int>();

            nums.Add(1);
            nums.Add(2);
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2]"));
            // Assert.AreEqual(1, nums.Count);
        }

        [Test]
        public void Test_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();

            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_GetByIndexElement()
        {
            // Arrange
            var names = new Collection<string>("Peter", "Maria");

            //Act
            var itemOnIdx0 = names[0];
            var itemOnIdx1 = names[1];

            //Assert
            Assert.That(itemOnIdx0, Is.EqualTo("Peter"));
            Assert.That(itemOnIdx1, Is.EqualTo("Maria"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndexElement()
        {
            var names = new Collection<string>("Bob", "Joe");

            Assert.That(() => { var name = names[-1]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString, Is.EqualTo("[Bob, Joe]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Teddy", "Gerry");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();

            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();
            Assert.That(nested.ToString(),
                Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));

        }
        [Test]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);

            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [TestCase("Peter", 0, "Peter")]
        [TestCase("Peter,Maria,Steve", 0, "Peter")]   //EntryData,  Index, expectedValue
        [TestCase("Peter,Maria,Steve", 1, "Maria")]
        [TestCase("Peter,Maria,Steve", 2, "Steve")]
        public void Test_Collection_GetByValidIndexElement(string data, int idx, string expectedValue)
        {
            var items = new Collection<string>(data.Split(","));
            var item = items[idx];
            Assert.That(item, Is.EqualTo(expectedValue));
        }

        [TestCase("", 0)]  // data, idx //
        [TestCase("Peter", -1)]
        [TestCase("Peter", 1)]
        [TestCase("Peter,Maria,Steve", -1)]
        [TestCase("Peter,Maria,Steve", 3)]
        [TestCase("Peter,Maria,Steve", 150)]
        public void Test_Collection_GetByInvalidIndexGivenElement(string data, int idx)
        {
            var items = new Collection<string>(data.Split(
                ",", StringSplitOptions.RemoveEmptyEntries));
            Assert.That(() => items[idx], Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            var removedItem = names.RemoveAt(1);
            Assert.That(removedItem, Is.EqualTo("Maria"));
            Assert.That(3, Is.EqualTo(names.Count));
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Steve, Mia]"));
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            var removedItem = names.RemoveAt(0);
            Assert.That(removedItem, Is.EqualTo("Peter"));
            Assert.That(3, Is.EqualTo(names.Count));
            Assert.That(names.ToString(), Is.EqualTo("[Maria, Steve, Mia]"));
        }

        /// DimiMitev lesson explanation Examples with Tests
        /// 
        [Test]
        public void Test_Collection_EmptyCtor()
        {
            //Arrange
            var nums = new Collection<int>();
            //Act

            // Assert
            Assert.That(nums.Count == 0, "CountProperty");
            //Assert.That(nums.Capacity == 0, "CapacityProperty");
            Assert.AreEqual(nums.Capacity, 16, "CapacityProperty");
            Assert.That(nums.ToString() == "[]");
        }

        [Test]
        public void Test_Collection_CtorSingleItem()
        {
            // Arrange
            var nums = new Collection<int>(5);
            //Act

            //Assert
            Assert.That(nums.Count == 1, "CountProperty");
            Assert.That(nums.ToString() == "[5]");
            Assert.AreEqual(nums.Capacity, 16, "CapacityProperty");

        }

        [Test]
        public void Test_Collection_CtorMultipleItemsInt()
        {
            var nums = new Collection<int>(5, 6);

            // Assert.That(nums.Count == 0, "CountProperty");
            Assert.AreEqual(nums.Count, 2, "Countproperty");
            Assert.That(nums.ToString() == "[5, 6]");
            Assert.AreEqual(nums.Capacity, 16, "CapacityProperty");

        }

        [Test]
        public void Test_Collection_CtorMultipleItemsString()
        {
            var nums = new Collection<string>("QA");

            // Assert.That(nums.Count == 0, "CountProperty");
            Assert.AreEqual(nums.Count, 1, "Countproperty");
            Assert.That(nums.ToString() == "[QA]");
            Assert.AreEqual(nums.Capacity, 16, "CapacityProperty");

        }

        [Test]
        public void Test_Collection_AddMethod()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act
            nums.Add(7);

            // Assert
            Assert.That(nums.Count == 1, "CountProperty");
            Assert.AreEqual(nums.Capacity, 16, "Capacityproperty");
            Assert.That(nums.ToString() == "[7]");
        }

        [Test]
        [Order(1)] //the order by running the test
        public void Test_Collection_AddMethod_RangeIntegers()
        {
            var items = new int[] { 6, 7, 8 };
            var nums = new Collection<int>();

            nums.AddRange(items);

            Assert.That(nums.Count == 3, "CountProperty");
            Assert.AreEqual(nums.Capacity, 16, "CapacityProperty");
            Assert.That(nums.ToString() == "[6, 7, 8]");

        }

        [Test]
        public void Test_Collection_AddWithGrowWithInteger() 
        {
            Collection<int> nums = new Collection<int>();
            int[] items = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            nums.AddRange(items);

            Assert.That(nums.Count == 18, "PropertyCount");
            Assert.AreEqual(nums.Capacity, 32);
            Assert.That(
                nums.ToString() == "[1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9]");
        }

        [Test]
        public void Test_Collection_1MillionItemsAdded()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);

            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [TestCase("Peter", 0, "Peter")]
        [TestCase("Ivan, Maria", 1, "Maria")]  //One and same test is run with diff dataCombinations
        public void Test_Collection_GetByValidIdx(
            string data, int numIdx, string expectedValue) 
        {
            // Arrange
            var nums = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));
            //Add
            var actual = nums[numIdx];
            //Assert

            Assert.AreEqual(expectedValue, actual);
        }

    }
}