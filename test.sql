            --  List all football leagues
            -- - What team does Levi Kelly work for?
            -- - List all international leagues.
            -- - List all the players for the Calgary Blackhawks
            -- - How many teams are in the Atlantic Amateur Field Hockey League?

SELECT * FROM Leagues WHERE Leagues.Sport == "Football";
SELECT * FROM Players WHERE FirstName == "Levi" AND LastName == "Kelly";
SELECT * FROM Teams WHERE Teams.TeamId == 16;
SELECT * FROM Leagues WHERE Leagues.Name LIKE '%International%';
SELECT * FROM Players WHERE TeamId == 8;
--ROUND 2
-- List all teams with a player named "Daniel".
-- List all *current* players on all teams that begin with 'R'; list the name of each players team.
SELECT * FROM Teams
LEFT JOIN Players ON Players.TeamId = Teams.TeamId
WHERE Players.FirstName =="Daniel";
SELECT * FROM Players
LEFT JOIN Teams ON Players.TeamId = Teams.TeamId
WHERE Teams.TeamName LIKE 'R%';

SELECT Leagues.Name, Players.FirstName, Players.LastName FROM Leagues
LEFT JOIN Teams ON Leagues.LeagueId = Teams.LeagueId
LEFT JOIN PlayerTeams ON PlayerTeams.TeamId = Teams.TeamId
LEFT JOIN Players ON Players.PlayerId = PlayerTeams.PlayerId
WHERE PLayers.FirstName == "Daniel"
ORDER BY Players.PlayerId;

