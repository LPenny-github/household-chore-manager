using ClassLab;
using Xunit;

namespace Tests
{
    public class CommandNewTests
    {
        [Fact]
        public void ReturnCommandNewResult_CommandLengthEqualToOne()
        {
            string[] testCommand = new string[]{"new"};
            string errorMessage = "未輸入家事名稱與頻率";
            var result = new CheckCommandNew().ReturnCommandNewResult(testCommand);
            Assert.False(result.IsValid);
            Assert.Contains(errorMessage, result.errorMessage);
            Assert.Equal(-1, result.frequency);
        }
        [Fact]
        public void ReturnCommandNewResult_CommandLengthEqualToTwo()
        {
            string[] testCommand = new string[]{"new","洗拖鞋"};
            string errorMessage = "未輸入家事頻率";
            var result = new CheckCommandNew().ReturnCommandNewResult(testCommand);
            Assert.False(result.IsValid);
            Assert.Contains(errorMessage, result.errorMessage);
            Assert.Equal(-1, result.frequency);
        }
        [Fact]
        public void ReturnCommandNewResult_CommandLengthMoreThanThree()
        {
            string[] testCommand = new string[]{"new","洗拖鞋","20","400"};
            string errorMessage = "輸入過多參數或詞彙、數組間有多餘空白";
            var result = new CheckCommandNew().ReturnCommandNewResult(testCommand);
            Assert.False(result.IsValid);
            Assert.Contains(errorMessage, result.errorMessage);
            Assert.Equal(-1, result.frequency);
        }
        [Fact]
        public void ReturnCommandNewResult_FrequencyLessThanOne()
        {
            string[] testCommand = new string[]{"new","洗拖鞋","0"};
            string errorMessage = $"家事頻率({testCommand[2]})不合理";
            var result = new CheckCommandNew().ReturnCommandNewResult(testCommand);
            Assert.False(result.IsValid);
            Assert.Contains(errorMessage, result.errorMessage);
            Assert.Equal(0, result.frequency);
        }
        [Fact]
        public void ReturnCommandNewResult_FrequencyIsEmpty()
        {
            string[] testCommand = new string[]{"new","洗拖鞋",""};
            string errorMessage = $"家事頻率({testCommand[2]})不合理";
            var result = new CheckCommandNew().ReturnCommandNewResult(testCommand);
            Assert.False(result.IsValid);
            Assert.Contains(errorMessage, result.errorMessage);
            Assert.Equal(0, result.frequency);
        }
        [Fact]
        public void ReturnCommandNewResult_FrequencyIsNotNumber()
        {
            string[] testCommand = new string[]{"new","洗拖鞋","%%%"};
            string errorMessage = $"家事頻率({testCommand[2]})不合理";
            var result = new CheckCommandNew().ReturnCommandNewResult(testCommand);
            Assert.False(result.IsValid);
            Assert.Contains(errorMessage, result.errorMessage);
            Assert.Equal(0, result.frequency);
        }
        [Fact]
        public void ReturnCommandNewResult_FrequencyIsOutOfIntLimit()
        {
            string[] testCommand = new string[]{"new","洗拖鞋","2,147,483,648"};
            string errorMessage = $"家事頻率({testCommand[2]})不合理";
            var result = new CheckCommandNew().ReturnCommandNewResult(testCommand);
            Assert.False(result.IsValid);
            Assert.Contains(errorMessage, result.errorMessage);
            Assert.Equal(0, result.frequency);
        }
        [Fact]
        public void ReturnCommandNewResult_ValidCommand()
        {
            string[] testCommand = new string[]{"new","洗拖鞋","28"};
            var result = new CheckCommandNew().ReturnCommandNewResult(testCommand);
            Assert.True(result.IsValid);
            Assert.Null(result.errorMessage);
            Assert.Equal(28, result.frequency);
        }
    }
}
