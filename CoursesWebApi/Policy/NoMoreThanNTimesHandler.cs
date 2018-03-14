using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesWebApi.Policy
{
    // Example of a dynamic evaluation Api : should use a DB to track user usage but statickeyword is enough for a demo
    public class NoMoreThanNTimesHandler : AuthorizationHandler<NoMoreThanNTimes>
    {
        private static int callCount = 0;
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NoMoreThanNTimes requirement)
        {
            if (callCount < requirement.MaxTimes)
            {
                ++callCount;
                context.Succeed(requirement);
            }
            //TODO: Use the following if targeting a version of
            //.NET Framework older than 4.6:
            //      return Task.FromResult(0);
            return Task.CompletedTask;
        }
    }
}
