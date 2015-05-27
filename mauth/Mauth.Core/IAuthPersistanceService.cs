using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mauth.Core
{
    public interface IAuthPersistanceService
    {
        IEnumerable<UserCredentials> Get(Expression<Func<UserCredentials, bool>> predicate);

        void Save(UserCredentials credentials);

        void Delete(string username);

        void Update(UserCredentials credentials);
    }
}
