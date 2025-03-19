namespace OpenCCNET.Test
{
    public class ZhConvertTest
    {
        public ZhConvertTest()
        {
            ZhConverter.Initialize();
        }

        private const string TextHans = """
            在这个信息发达的时代，每家每户的人们都对于个人形象愈发重视。
            无论是发型、穿搭，还是言行举止，都可能影响社交场合的表现。
            因此，许多人会修剪网络热门视频推荐的发型，让自己保持最佳状态。
            当然，时间管理亦不可忽视，懂得合理运用时间的人，总能发挥最大的效率。
            有些人对于传统钟表情有独钟，认为里面的细节展现了精湛的工艺与独特的魅力。
            """;

        private const string TextHant = """
            在這個信息發達的時代，每家每戶的人們都對於個人形象愈發重視。
            無論是髮型、穿搭，還是言行舉止，都可能影響社交場合的表現。
            因此，許多人會修剪網絡熱門視頻推薦的髮型，讓自己保持最佳狀態。
            當然，時間管理亦不可忽視，懂得合理運用時間的人，總能發揮最大的效率。
            有些人對於傳統鐘錶情有獨鍾，認爲裏面的細節展現了精湛的工藝與獨特的魅力。
            """;

        private const string TextTw = """
            在這個信息發達的時代，每家每戶的人們都對於個人形象愈發重視。
            無論是髮型、穿搭，還是言行舉止，都可能影響社交場合的表現。
            因此，許多人會修剪網絡熱門視頻推薦的髮型，讓自己保持最佳狀態。
            當然，時間管理亦不可忽視，懂得合理運用時間的人，總能發揮最大的效率。
            有些人對於傳統鐘錶情有獨鍾，認為裡面的細節展現了精湛的工藝與獨特的魅力。
            """;

        private const string TextTwIdiom = """
            在這個資訊發達的時代，每家每戶的人們都對於個人形象愈發重視。
            無論是髮型、穿搭，還是言行舉止，都可能影響社交場合的表現。
            因此，許多人會修剪網路熱門影片推薦的髮型，讓自己保持最佳狀態。
            當然，時間管理亦不可忽視，懂得合理運用時間的人，總能發揮最大的效率。
            有些人對於傳統鐘錶情有獨鍾，認為裡面的細節展現了精湛的工藝與獨特的魅力。
            """;

        private const string TextHk = """
            在這個信息發達的時代，每家每户的人們都對於個人形象愈發重視。
            無論是髮型、穿搭，還是言行舉止，都可能影響社交場合的表現。
            因此，許多人會修剪網絡熱門視頻推薦的髮型，讓自己保持最佳狀態。
            當然，時間管理亦不可忽視，懂得合理運用時間的人，總能發揮最大的效率。
            有些人對於傳統鐘錶情有獨鍾，認為裏面的細節展現了精湛的工藝與獨特的魅力。
            """;

        [Fact]
        public void Test_ToHantFromHans_Success()
        {
            Assert.Equal(TextHant, TextHans.ToHantFromHans());
        }

        [Fact]
        public void Test_ToHansFromHant_Success()
        {
            Assert.Equal(TextHans, TextHant.ToHansFromHant());
        }

        [Fact]
        public void Test_ToTWFromHans_Success()
        {
            Assert.Equal(TextTw, TextHans.ToTWFromHans());
        }

        [Fact]
        public void Test_ToTWFromHans_IsIdiomConvertTrue_Success()
        {
            Assert.Equal(TextTwIdiom, TextHans.ToTWFromHans(true));
        }

        [Fact]
        public void Test_ToHansFromTW_Success()
        {
            Assert.Equal(TextHans, TextTw.ToHansFromTW());
        }

        [Fact]
        public void Test_ToHansFromTW_IsIdiomConvertTrue_Success()
        {
            Assert.Equal(TextHans, TextTwIdiom.ToHansFromTW(true));
        }

        [Fact]
        public void Test_ToHKFromHans_Success()
        {
            Assert.Equal(TextHk, TextHans.ToHKFromHans());
        }

        [Fact]
        public void Test_ToHansFromHK_Success()
        {
            Assert.Equal(TextHans, TextHk.ToHansFromHK());
        }

        [Fact]
        public void Test_ToTWFromHant_Success()
        {
            Assert.Equal(TextTw, TextHant.ToTWFromHant());
        }

        [Fact]
        public void Test_ToTWFromHant_IsIdiomConvertTrue_Success()
        {
            Assert.Equal(TextTwIdiom, TextHant.ToTWFromHant(true));
        }

        [Fact]
        public void Test_ToHantFromTW_Success()
        {
            Assert.Equal(TextHant, TextTw.ToHantFromTW());
        }

        [Fact]
        public void Test_ToHantFromTW_IsIdiomConvertTrue_Success()
        {
            Assert.Equal(TextHant, TextTwIdiom.ToHantFromTW(true));
        }

        [Fact]
        public void Test_ToHKFromHant_Success()
        {
            Assert.Equal(TextHk, TextHant.ToHKFromHant());
        }

        [Fact]
        public void Test_ToHantFromHK_Success()
        {
            Assert.Equal(TextHant, TextHk.ToHantFromHK());
        }
    }
}