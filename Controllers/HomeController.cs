using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.WomensLeague = _context.Leagues
                .Where(league => league.Name.Contains("Women"));//Women Leagues

            ViewBag.HockeyLeague = _context.Leagues
                .Where(league => league.Sport.Contains("Hockey"));//Hockey

            ViewBag.NonFootballLeague = _context.Leagues
                .Where(league => league.Sport != "Football");//Everything but football

            ViewBag.ConferenceLeagues = _context.Leagues
                .Where(league => league.Name.Contains("Conference"));//Conference Leagues

            ViewBag.AtlanticLeagues = _context.Leagues
                .Where(league => league.Name.Contains("Atlantic"));//Atlantic Leagues

            ViewBag.DallasTeams = _context.Teams
                .Where(team => team.Location == "Dallas");//Dallas Location

            ViewBag.TeamRaptors = _context.Teams
                .Where(team =>team.TeamName == "Raptors");//Raptors name

            ViewBag.HaveCityInName = _context.Teams
                .Where(team => team.Location.Contains("City"));//Cities in name
            
            ViewBag.StartWithT = _context.Teams
                .Where(team => team.TeamName.StartsWith("T"));//Team starts with T

            ViewBag.AlphaLocation = _context.Teams
                .OrderBy(team => team.Location);//Location order

            ViewBag.ReverseOrder = _context.Teams
                .OrderByDescending(team => team.TeamName);//Team Order

            ViewBag.CooperPlayers = _context.Players
                .Where(player => player.LastName == "Cooper");//Cooper last name

            ViewBag.JoshuaPlayers = _context.Players
                .Where(player => player.FirstName == "Joshua");//Where is Joshua

            ViewBag.CooperNotJoshua = _context.Players
                .Where(player => player.LastName == "Cooper" && player.FirstName != "Joshua");//Where is Joshua Cooper

            ViewBag.AlexanderOrWyatt = _context.Players
                .Where(player => player.FirstName == "Alexander" || player.FirstName == "Wyatt");//Where is Alexander or Wyatt?

            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            
            ViewBag.ASC = _context.Teams//Atlantic Soccer Conference
                .Where(l => l.LeagueId == 5)
                .ToList();
            
            
            ViewBag.Boston = _context.Players//current players on boston penguins
                .Where(l => l.TeamId == 2)
                .ToList();   


            ViewBag.PlayersFromICBC = _context.Players//International Collegiate Baseball Conference
            .Include(players => players.CurrentTeam)
            .ThenInclude(currentTeam => currentTeam.CurrLeague)
            .Where(
                player => player.CurrentTeam.CurrLeague.Name == "International Collegiate Baseball Conference"
                );
            
            ViewBag.LopezACAF = _context.Players//players with name Lopez in a certain league
            .Include(players => players.CurrentTeam)
            .ThenInclude(currentTeam => currentTeam.CurrLeague)
            .Where(
                player => player.CurrentTeam.CurrLeague.Name == "American Conference of Amateur Football"
                && player.LastName == "Lopez"
                )
            .ToList();

            ViewBag.FootballPlayers = _context.Players//Football players (all)
            .Include(players => players.CurrentTeam)
            .ThenInclude(currentTeam => currentTeam.CurrLeague)
            .Where(
                player => player.CurrentTeam.CurrLeague.Sport == "Football"
                );

            ViewBag.TeamsWSophia = _context.Teams//teams with a player named sophia
            .Include(team => team.CurrentPlayers)
            .Where(team => team.CurrentPlayers
            .Any(player => player.FirstName == "Sophia"));

            ViewBag.LeaguesWSophia = _context.Leagues//leagues with a player named sophia
            .Include(league => league.Teams)
            .ThenInclude(team => team.CurrentPlayers)
            .Where(league => league.Teams
                .Any(team => team.CurrentPlayers
                .Any(player => player.FirstName == "Sophia")));
            
            ViewBag.FloresNoRoughRiders = _context.Players//people who have flores as a last name and are not current
            .Where(player => player.TeamId != 1 && player.LastName == "Flores");
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}