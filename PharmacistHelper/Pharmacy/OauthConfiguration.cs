using System.Collections.Generic;


namespace Pharmacy
{
    public class OauthConfiguration
    {
        public Dictionary<KeyDict, ValDict> Dict = new Dictionary<KeyDict, ValDict>();
    }

    public class KeyDict
    {
        private readonly string _email;
        private readonly string _scope;
        
        public KeyDict(string email, string scope)
        {
            _email = email;
            _scope = scope;
        }

        protected bool Equals(KeyDict other)
        {
            return string.Equals(_email, other._email) && Equals(_scope, other._scope);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyDict)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_email != null ? _email.GetHashCode() : 0) * 397) ^ (_scope != null ? _scope.GetHashCode() : 0);
            }
        }
    }
    public class ValDict
    {
        private readonly string _uri;
        private readonly string _token;
        public string GetUri()
        {
            return _uri;
        }
        public string GetToken()
        {
            return _token;
        }
        public ValDict(string uri, string token)
        {
            _uri = uri;
            _token = token;
        }

        protected bool Equals(ValDict other)
        {
            return string.Equals(_token, other._token) && string.Equals(_uri, other._uri);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ValDict)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_token != null ? _token.GetHashCode() : 0) * 397) ^ (_uri != null ? _uri.GetHashCode() : 0);
            }
        }
    }
}