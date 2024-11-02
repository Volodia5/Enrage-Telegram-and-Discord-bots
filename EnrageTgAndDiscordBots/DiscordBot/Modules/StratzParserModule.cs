﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using STRATZ;

namespace EnrageDiscordTournamentBot.Modules
{
    public class StratzParserModule
    {
        public string GetRank(string userAccountId)
        {
            // string url = $"https://stratz.com/players/{userAccountId}";
            // WebClient webClient = new WebClient();
            // string html = webClient.DownloadString(url);
            //
            // HtmlDocument htmlDoc = new HtmlDocument();
            // htmlDoc.LoadHtml(html);
            //
            // HtmlNodeCollection userAccountNode = htmlDoc.DocumentNode.SelectNodes("//div[@class='hitagi__sc-1ah81hi-0 hitagi__sc-19ps7xc-0  eTLPcy hitagi__sc-1qujzc6-6 fZGbpD']");
            //
            // string userDotaRankInnerHtml = userAccountNode[2].InnerHtml.ToString();
            // var userDotaAccountData = userDotaRankInnerHtml.Split("https://cdn.stratz.com/images/dota2/seasonal_rank/");
            // string userAccountRating = userDotaAccountData[1].Split(".png")[0];

            DotaQueryQueryBuilder query = new DotaQueryQueryBuilder()
                .WithPlayer(
                    new PlayerTypeQueryBuilder()
                        .WithSteamAccount(new SteamAccountTypeQueryBuilder()
                            .WithId()
                            .WithName()
                            .WithProfileUri()
                            .WithAvatar()
                            .WithIsDotaPlusSubscriber()
                            .WithDotaAccountLevel()
                            .WithIsAnonymous()
                            .WithSeasonRank()
                            .WithSeasonLeaderboardRank()
                            .WithSeasonLeaderboardDivisionId()
                            .WithProSteamAccount(new ProSteamAccountTypeQueryBuilder()
                                .WithName()
                                .WithRealName())
                        )
                        .WithMatchCount()
                        .WithWinCount()
                        .WithFirstMatchDate()
                        .WithLastMatchDate()
                    ,
                    long.Parse(userAccountId));
            
            var sdfs = query.Build(Formatting.Indented);
            
            
            return "123";
        }
    }
}
