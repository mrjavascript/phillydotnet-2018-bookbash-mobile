using System;
using System.Collections.Specialized;

namespace BookBash.Utility
{
    public class UriBuilderExt
    {
        private readonly NameValueCollection _collection;
        private readonly UriBuilder _builder;

        public UriBuilderExt(string uri)
        {
            _builder = new UriBuilder(uri);
            _collection = System.Web.HttpUtility.ParseQueryString(string.Empty);
        }

        public void AddParameter(string key, string value)
        {
            _collection.Add(key, value);
        }

        public Uri Uri
        {
            get
            {
                _builder.Query = _collection.ToString();
                return _builder.Uri;
            }
        }

    }
}