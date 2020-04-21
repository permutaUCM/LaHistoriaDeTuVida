namespace LHDTV.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string PassworSecret { get; set; }


        //Token life span in hours
        public int TokenLifeSpan { get; set; }

        public string EmailUser { get; set; }
        public string EmailPassw { get; set; }
        public string SmtpClient { get; set; }
        public int SmtpPort { get; set; }
    }
}