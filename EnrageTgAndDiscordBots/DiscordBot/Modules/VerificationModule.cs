using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using EnrageDiscordTournamentBot.Log;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Model.Dotabuff;
using DotA2StatsParser.Model.Dotabuff.Interfaces;
using DotA2StatsParser.Model.HealthCheck.Interfaces;
using DotA2StatsParser.Model.Yasp.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using OpenDotaDotNet;

namespace EnrageDiscordTournamentBot.Modules
{
    public class VerificationModule : InteractionModuleBase<SocketInteractionContext>
    {
        private Logger _logger;
        private DiscordSocketClient _client;

        public VerificationModule(ConsoleLogger logger, DiscordSocketClient client)
        {
            _logger = logger;
            _client = client;

            _client.MessageReceived += OnMessageRecieved;
        }

        [DefaultMemberPermissions(GuildPermission.Administrator)]
        [SlashCommand("verif-users", "Верифицирует игроков")]
        public async Task VerifUsers(int countmessages)
        {
            var guild = _client.GetGuild(1075718003578126386);
            ITextChannel finishRegistrationChannel = (ITextChannel)_client.GetChannel(1286991552526417992);
            IEnumerable<IMessage> messages = await finishRegistrationChannel
                .GetMessagesAsync(countmessages, CacheMode.AllowDownload).FlattenAsync();
            foreach (var item in messages)
                {
                    VerifOnCommand(item);
                }
            // if (us_id == null)
            // {
            //     foreach (var item in messages)
            //     {
            //         VerifOnCommand(item);
            //     }
            // }
            // else
            // {
            //     foreach (var item in messages)
            //     {
            //         VerifOnCommand(item, us_id);
            //     }
            // }

            await RespondAsync("Пользователи успешно верифицированы", ephemeral: true);
        }

        private async Task OnMessageRecieved(SocketMessage message)
        {
            IGuildUser user = (IGuildUser)message.Author;
            var userPermissoions = user.GetPermissions((IGuildChannel)message.Channel);

            if (userPermissoions.ManageChannel != true)
            {
                VerifOnCommand((IMessage)message);
            }
            else
            {
                return;
            }
        }

        private async Task VerifOnCommand(IMessage message)
        {
            IGuildUser user = (IGuildUser)message.Author;
            var guild = _client.GetGuild(1075718003578126386);
            IEmote emote = await guild.GetEmoteAsync(1210198064669917184);
            //StratzParserModule parserModule = new StratzParserModule();
            string splitString;
            string userAccountRatingString;
            int userAccountRatingInt = 0;
            bool parseResult;
            //parserModule.GetRank("76561198016430884");
            //string userParsedRank;
            // if (us_id != null)
            // {
            //     OpenDotaApi(us_id.ToString());
            // }

            if (message.Channel.Id == 1286991552526417992)
            {
                string messageText = message.Content.ToString();
                int tgPhotoInMessage = message.Attachments.Count;

                // try
                // {
                //     if (tgPhotoInMessage == 1)
                //     {
                //         splitString = messageText.Split("2)")[1];
                //
                //         OpenDotaApi(splitString);
                //         await AddingRoleVerife(message, splitString, messageText, user, emote);
                //         return;
                //     }
                //     else
                //     {
                //         await WriteExeptIncorrectVerifMessage(null, message, messageText);
                //     }
                // }
                // catch (Exception exept)
                // {
                //     try
                //     {
                //         if (tgPhotoInMessage == 1)
                //         {
                //             splitString = messageText.Split("2.")[1];
                //
                //             OpenDotaApi(splitString);
                //             await AddingRoleVerife(message, splitString, messageText, user, emote);
                //             return;
                //         }
                //     }
                //     catch (Exception ex)
                //     {
                //         try
                //         {
                //             if (tgPhotoInMessage == 1)
                //             {
                //                 userAccountRatingString = messageText.Split("\n")[1];
                //                 parseResult = int.TryParse(userAccountRatingString.Trim(), out userAccountRatingInt);
                //
                //                 if (parseResult == false)
                //                 {
                //                     await WriteExeptIncorrectVerifMessage(null, message, messageText);
                //                 }
                //
                //                 //userParsedRank = parser.GetRank(userAccountId);
                //                 //userRank = userParsedRank.Split("_")[1];
                //                 OpenDotaApi(splitString);
                //                 await AddingRolesToUser(null, message, user, emote, userAccountRatingInt);
                //                 return;
                //             }
                //         }
                //         catch (Exception e)
                //         {
                //             if (tgPhotoInMessage == 1)
                //             {
                //                 using (var httpClient = new System.Net.Http.HttpClient())
                //                 {
                //                     using (var attachment =
                //                            await httpClient.GetStreamAsync(message.Attachments.First().Url))
                //                     {
                //                         await WriteExeptIncorrectVerifMessage(null, message, messageText);
                //                     }
                //                 }
                //             }
                //             else
                //             {
                //                 await WriteExeptIncorrectVerifMessage(null, message, messageText);
                //             }
                //         }
                //     }
                // }
                
                try
                {
                    if (tgPhotoInMessage == 1)
                    {
                        splitString = messageText.Split("1)")[1];

                        OpenDotaApi(splitString);
                        await AddingRoleVerife(message, splitString, messageText, user, emote);
                        return;
                    }
                    else
                    {
                        await WriteExeptIncorrectVerifMessage(null, message, messageText);
                    }
                }
                catch (Exception exept)
                {
                    try
                    {
                        if (tgPhotoInMessage == 1)
                        {
                            splitString = messageText.Split("1.")[1];

                            OpenDotaApi(splitString);
                            await AddingRoleVerife(message, splitString, messageText, user, emote);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (tgPhotoInMessage == 1)
                            {
                                userAccountRatingString = messageText.Split("\n")[1];
                                parseResult = int.TryParse(userAccountRatingString.Trim(), out userAccountRatingInt);

                                if (parseResult == false)
                                {
                                    await WriteExeptIncorrectVerifMessage(null, message, messageText);
                                }

                                //userParsedRank = parser.GetRank(userAccountId);
                                //userRank = userParsedRank.Split("_")[1];
                                await AddingRolesToUser(null, message, user, emote, userAccountRatingInt);
                                return;
                            }
                        }
                        catch (Exception e)
                        {
                            if (tgPhotoInMessage == 1)
                            {
                                using (var httpClient = new System.Net.Http.HttpClient())
                                {
                                    using (var attachment =
                                           await httpClient.GetStreamAsync(message.Attachments.First().Url))
                                    {
                                        await WriteExeptIncorrectVerifMessage(null, message, messageText);
                                    }
                                }
                            }
                            else
                            {
                                await WriteExeptIncorrectVerifMessage(null, message, messageText);
                            }
                        }
                    }
                }
            }
        }

        private static async Task AddingRoleVerife(IMessage message, string splitString, string messageText,
            IGuildUser user,
            IEmote emote)
        {
            string userAccountRatingString;
            bool parseResult;
            int userAccountRatingInt;
            userAccountRatingString = splitString.Split("\n")[0];
            parseResult = int.TryParse(userAccountRatingString.Trim(), out userAccountRatingInt);

            if (parseResult == false)
            {
                await WriteExeptIncorrectVerifMessage(null, message, messageText);
            }

            //userParsedRank = parser.GetRank(userAccountId);
            //userRank = userParsedRank.Split("_")[1];
            OpenDotaApi(splitString);
            await AddingRolesToUser(null, message, user, emote, userAccountRatingInt);
        }

        private static async Task WriteExeptIncorrectVerifMessage(SocketMessage? message, IMessage? iMassage,
            string messageText)
        {
            if (message != null)
            {
                if (message.Attachments.Count == 1)
                {
                    using (var httpClient = new System.Net.Http.HttpClient())
                    {
                        using (var attachment = await httpClient.GetStreamAsync(message.Attachments.First().Url))
                        {
                            await message.Author.SendMessageAsync(
                                $"Некорректная заявка на верификацию. Укажите свои данные еще раз. Просьба указывать свои данные согласно примеру:\n1) id аккаунта\n2) количество ммр\n3) прикрепленний скриншот подписки на наш тг канал. \n\n Ваша заявка была - {messageText}. \n**Заявка пишется 1 целым сообщением**");
                            await message.Author.SendMessageAsync("Ваш скриншот  👇");
                            await message.Author.SendFileAsync(attachment, "our_screenshot.png");
                            await message.Author.SendMessageAsync(
                                "Если вас уже верифицировали и это сообщение пришло ошибочно - проигнорируйте его");
                            await message.DeleteAsync();
                        }
                    }
                }
                else
                {
                    await message.Author.SendMessageAsync(
                        $"Некорректная заявка на верификацию. Укажите свои данные еще раз. Просьба указывать свои данные согласно примеру:\n1) id аккаунта\n2) количество ммр\n3) прикрепленний скриншот подписки на наш тг канал. \n\n Ваша заявка была - {messageText}. \n**Заявка пишется 1 целым сообщением**");
                    await message.Author.SendMessageAsync(
                        "Если вас уже верифицировали и это сообщение пришло ошибочно - проигнорируйте его");
                    await message.DeleteAsync();
                }
            }
            else
            {
                if (iMassage.Attachments.Count == 1)
                {
                    using (var httpClient = new System.Net.Http.HttpClient())
                    {
                        using (var attachment = await httpClient.GetStreamAsync(iMassage.Attachments.First().Url))
                        {
                            await iMassage.Author.SendMessageAsync(
                                $"Некорректная заявка на верификацию. Укажите свои данные еще раз. Просьба указывать свои данные согласно примеру:\n1) id аккаунта\n2) количество ммр\n3) прикрепленний скриншот подписки на наш тг канал. \n\n Ваша заявка была - {messageText}. \n**Заявка пишется 1 целым сообщением**");
                            await iMassage.Author.SendMessageAsync("Ваш скриншот  👇");
                            await iMassage.Author.SendFileAsync(attachment, "our_screenshot.png");
                            await iMassage.Author.SendMessageAsync(
                                "Если вас уже верифицировали и это сообщение пришло ошибочно - проигнорируйте его");
                            await iMassage.DeleteAsync();
                        }
                    }
                }
                else
                {
                    await iMassage.Author.SendMessageAsync(
                        $"Некорректная заявка на верификацию. Укажите свои данные еще раз. Просьба указывать свои данные согласно примеру:\n1) id аккаунта\n2) количество ммр\n3) прикрепленний скриншот подписки на наш тг канал. \n\n Ваша заявка была - {messageText}. \n**Заявка пишется 1 целым сообщением**");
                    await iMassage.Author.SendMessageAsync(
                        "Если вас уже верифицировали и это сообщение пришло ошибочно - проигнорируйте его");
                    await iMassage.DeleteAsync();
                }
            }
        }

        private static int OpenDotaApi(string splitString)
        {
            Console.WriteLine("Вход в апи :", splitString);
            using (DotA2StatsParser.Dataparser dataparser = new DotA2StatsParser.Dataparser())
            {
                IHealthCheckResult result = dataparser.PerformHealthCheck();

                if (result.IsDotabuffAvailable)
                {
                    IPlayer player1 = dataparser.GetPlayerPageData($"{splitString}");
                    Console.WriteLine(player1.ToString());
                    IEnumerable<IMatchExtended> matchList = dataparser.GetPlayerMatchesPageData(player1.Id,
                        new PlayerMatchesOptions() { Duration = Durations.Between20And30 });
                }

                if (result.IsYaspAvailable)
                {
                    IEnumerable<IWordCloud> playerWordCount = dataparser.GetWordCloud($"{splitString}");
                    foreach (var item in playerWordCount)
                    {
                        Console.WriteLine(item.Word);
                    }
                }
            }

            return -1;
        }

        private static async Task AddingRolesToUser(SocketMessage? message, IMessage? iMessage, IGuildUser user,
            IEmote emote, int userRank)
        {
            //switch (userRank)
            //{
            //    case "0":
            //        await user.SendMessageAsync("Вам выданы все роли, за исключением роли ранга, т.к. у вас отсутствует ранг.\nЕсли у вас присутствует ранг напишите администратору @volod1a_volod1a");
            //        await user.AddRoleAsync(1096163693806502018);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "1":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096124412832530452);
            //        await user.RemoveRoleAsync(1206360732292223016);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "2":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096124472983031878);
            //        await user.RemoveRoleAsync(1206360732292223016);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "3":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096124783382507530);
            //        await user.RemoveRoleAsync(1206360732292223016);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "4":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096124828743909446);
            //        await user.RemoveRoleAsync(1206360732292223016);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "5":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096124878110851082);
            //        await user.RemoveRoleAsync(1206360732292223016);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "6":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096124920154574868);
            //        await user.RemoveRoleAsync(1206360732292223016);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "7":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096124957379002380);
            //        await user.RemoveRoleAsync(1206360732292223016);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "8":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096125008931192832);
            //        await user.RemoveRoleAsync(1206360732292223016);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    case "8c":
            //        await user.AddRoleAsync(1096163693806502018);
            //        await user.AddRoleAsync(1096125287273598987);
            //        await user.RemoveRoleAsync(1096125508376334336);
            //        await message.AddReactionAsync(emote);
            //        break;
            //    default:
            //        return;
            //}


            if (userRank == 0)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 1 && userRank <= 770)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096124412832530452);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 770 && userRank <= 1540)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096124472983031878);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 1540 && userRank <= 2310)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096124783382507530);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 2310 && userRank <= 3080)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096124828743909446);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 3080 && userRank <= 3850)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096124878110851082);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 3850 && userRank <= 4620)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096124920154574868);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 4620 && userRank <= 6000)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096125008931192832);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 6000 && userRank <= 7000)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096125062702178454);
                await user.RemoveRoleAsync(1206360732292223016);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 7000 && userRank <= 8000)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096125287273598987);
                await user.RemoveRoleAsync(1096125508376334336);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 8000 && userRank <= 9000)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096125371709128814);
                await user.RemoveRoleAsync(1096125508376334336);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 9000 && userRank <= 10000)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096125446392918086);
                await user.RemoveRoleAsync(1096125508376334336);
                await iMessage.AddReactionAsync(emote);
            }
            else if (userRank > 10000)
            {
                await user.AddRoleAsync(1096163693806502018);
                await user.AddRoleAsync(1096125508376334336);
                await user.RemoveRoleAsync(1096125508376334336);
                await iMessage.AddReactionAsync(emote);
            }
        }
    }
}