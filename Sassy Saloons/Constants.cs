namespace Sassy_Saloons
{
    class Constants
    {
        #region bases
        private const string baseUrl                = "http://localhost:8888/";

        private const string privateKey             = "h8mDq5YIF1SWNHkArGmw";
        private const string publicKey              = "rQ9P5YLnqt8JEmarbyoo  ";
        #endregion


        #region Calls
        private const string salreg                 = baseUrl + "regSalon";
        private const string salcom                 = baseUrl + "salComment";
        private const string reguse                 = baseUrl + "regUser";
        private const string login                  = baseUrl + "login";
        private const string incom                  = baseUrl + "salComment";        

        internal const string SALREG                = salreg;
        internal const string SALCOM                = salcom;
        internal const string REGUSE                = reguse;
        internal const string LOGIN                 = login;
        internal const string INCOM                 = incom;        
        #endregion

        #region Get them
        private const string getuser                = baseUrl + "getUser";
        private const string getcom                 = baseUrl + "getComments";
        private const string getsal                 = baseUrl + "searchSaloon";

        internal const string GETUSER               = getuser;
        internal const string GETCOM                = getcom;
        internal const string GETSAL                = getsal;
        #endregion                

        #region Encryption
        private const string priv                   = privateKey + "PePvTs4Ks9Xxa9kqpJsc";
        private const string pub                    = publicKey + "YibRd1eBb71FnwAn93xI";

        internal const string PRIVATEKEY            = priv;
        internal const string PUBLICKEY             = pub;
        #endregion
    }
}
