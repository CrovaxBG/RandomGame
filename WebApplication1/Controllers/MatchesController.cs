using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication1.DataAccess;
using WebApplication1.DataAccess.DTOs;

namespace WebApplication1.Controllers
{
    [EnableCors("http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Matches")]
    public class MatchesController : ApiController
    {
        private Random _rand = new Random();
        private RandomGameEntities _dbContext;

        public MatchesController()//RandomGameEntities context
        {
            //TODO replace with DI
            //_dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbContext = new RandomGameEntities();
        }

        // GET api/matches/activeMatch
        [Route("activeMatch")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var match = _dbContext.ActiveMatches.Where(m => m.EndTime > DateTime.Now).ToArray().LastOrDefault();
            if (match == null)
            {
                var randomMatch = GetRandomElement(_dbContext.Matches);
                match = _dbContext.ActiveMatches.Add(new ActiveMatch
                {
                    MatchId = randomMatch.Id,
                    EndTime = DateTime.Now.Add(TimeSpan.FromSeconds(randomMatch.SignUpDuration))
                });
                _dbContext.SaveChanges();
            }

            return Json(new ActiveMatchDTO
            {
                EndTime = match.EndTime.Value,
                Id = match.Id,
                MatchId = match.MatchId
            });
        }

        [Route("joinMatch")]
        [HttpPost]
        public IHttpActionResult Post(string userId, int matchId)
        {

            var activeMatches = _dbContext.ActiveMatches.Where(m => m.MatchId == matchId && m.EndTime > DateTime.Now).ToArray();
            if (!activeMatches.Any())
            {
                return BadRequest("This match is not active!");
            }

            _dbContext.UserMatchScores.Add(new UserMatchScore
            {
                UserId = 
            })
        }

        private T GetRandomElement<T>(IEnumerable<T> source)
        {
            int index = _rand.Next(0, source.Count());
            return source.ElementAt(index);
        }
    }
}