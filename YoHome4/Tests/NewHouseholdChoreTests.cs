using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ClassLab;
using Xunit;

namespace Tests
{
    public class NewHouseholdChoreTests
    {
        [Fact]
        public void CreateHouseholdChore_DuplicatedHouseholdChoreName()
        {
            List<HouseholdChoreInformation> testContainer = new();

            HouseholdChoreInformation householdChoreData = new HouseholdChoreInformation
            {
                HouseholdChoreSerialNumber = 1,
                HouseholdChoreName = "洗棉被",
                IdealFrequency = 60,
                LastImplementedDate = new DateTime()
            };

            testContainer.Add(householdChoreData);

            var result = new NewHouseholdChore().CreateHouseholdChore("洗棉被", 2, testContainer);
            string errorMessage = "此家事已被建立";
            Assert.False(result.valid);
            Assert.Contains(errorMessage, result.errorMessage);
            Assert.Null(result.jsonString);
        }
        [Fact]
        public void CreateHouseholdChore_ValidHouseholdChoreData_ReturnRightDataType()
        {
            List<HouseholdChoreInformation> testContainer = new();

            var result = new NewHouseholdChore().CreateHouseholdChore("洗棉被", 60, testContainer);

            Assert.True(result.valid);
            Assert.Null(result.errorMessage);
            Assert.NotNull(result.jsonString);
        }
        [Fact]
        public void CreateHouseholdChore_ValidHouseholdChoreData_ReturnRightDataContents()
        {
            List<HouseholdChoreInformation> testContainer = new();

            var methodResult = new NewHouseholdChore().CreateHouseholdChore("洗棉被", 60, testContainer);
            
            var resultJsonToObject = new List<HouseholdChoreInformation>();

            if (!string.IsNullOrEmpty(methodResult.jsonString))
            {
                resultJsonToObject = JsonSerializer.Deserialize<List<HouseholdChoreInformation>>
                                                                        (methodResult.jsonString);
            }

            Assert.Equal(1, resultJsonToObject.Count);
            Assert.Equal(1, resultJsonToObject.First().HouseholdChoreSerialNumber);
            Assert.Equal("洗棉被", resultJsonToObject.First().HouseholdChoreName);
            Assert.Equal(60, resultJsonToObject.First().IdealFrequency);
            Assert.Equal(new DateTime(), resultJsonToObject.First().LastImplementedDate);
        }
        [Fact]
        public void CreateHouseholdChore_ValidHouseholdChoreData_ReturnRightSerialNumber()
        {
            List<HouseholdChoreInformation> testContainer = new();

            HouseholdChoreInformation householdChoreData = new HouseholdChoreInformation
            {
                HouseholdChoreSerialNumber = 1,
                HouseholdChoreName = "洗棉被",
                IdealFrequency = 60,
                LastImplementedDate = new DateTime()
            };

            testContainer.Add(householdChoreData);

            var methodResult = new NewHouseholdChore().CreateHouseholdChore("擦窗戶", 90, testContainer);
            
            var resultJsonToObject = new List<HouseholdChoreInformation>();

            if (!string.IsNullOrEmpty(methodResult.jsonString))
            {
                resultJsonToObject = JsonSerializer.Deserialize<List<HouseholdChoreInformation>>
                                                                        (methodResult.jsonString);
            }

            Assert.Equal(2, resultJsonToObject.Count);
            Assert.Equal(2, resultJsonToObject.Last().HouseholdChoreSerialNumber);
        }
    }
}