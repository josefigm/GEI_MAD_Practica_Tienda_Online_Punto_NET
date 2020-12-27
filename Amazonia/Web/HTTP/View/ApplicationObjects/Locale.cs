﻿namespace Es.Udc.DotNet.Amazonia.Web.HTTP.View.ApplicationObjects
{
    public struct Locale
    {
        private string language;
        private string country;

        #region Properties

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        #endregion Properties

        public Locale(string language)
        {
            this.language = language;
            this.country = "GB";
        }
    }
}