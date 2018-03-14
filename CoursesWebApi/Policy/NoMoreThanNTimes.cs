using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesWebApi.Policy
{
    /// Un IAuthorizationRequirement contient juste des données de validation concernant la stratégie
    public class NoMoreThanNTimes : IAuthorizationRequirement
    {
        public int MaxTimes { get; set; }
        public NoMoreThanNTimes(int maxTimes)
        {
            MaxTimes = maxTimes;
        }
    }
}
