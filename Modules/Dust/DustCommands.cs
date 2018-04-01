using BlockBreaker.App.Main.Utils.MathUtils;
using DustModule;
using DustModule.Items;
using Newtonsoft.Json;
using RazorwingGL.Framework.Configuration;
using RazorwingGL.Framework.IO.Network;
using RazorwingGL.Framework.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Commands;
using TwitchBot.Commands.Builders;
using TwitchBot;
using TwitchBot.API;
using TwitchBot.API.Commands;
using TwitchBot.Redstone;

namespace TwitchBot.Modules.Dust
{
    //[MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
    public class DustCommands : ModuleBase<CommandContext>
    {
        
        [Command("inventory"),Alias("inv", "инвентарь", "рюкзак"), Summary("Проверить что же у вас лежит в инвентаре"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task Inventory()
        {
            string inventoryText = "";

            foreach (var it in Context.User.Dust.Items)
            {
                if (it.ToString() != "")
                    inventoryText += it.ToString() + ", ";
            }

            if (inventoryText != "")
            {
                Reply($"@{Context.User.Nick} У вас есть {inventoryText}");
            }
            else
            {
                Reply($"@{Context.User.Nick} У вас нет предметов!");
            }
        }

        [Command("inventory"), Alias("inv", "инвентарь", "рюкзак"), Summary("Проверить что же у вас лежит в инвентаре"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot"), RequireUserPermission(ChannelPermission.Moderator)]
        public async Task Inventory(User target)
        {
            string inventoryText = "";

            foreach (var it in target.Dust.Items)
            {
                if (it.ToString() != "")
                    inventoryText += it.ToString() + ", ";
            }

            if (inventoryText != "")
            {
                Reply($"У {target.Nick} есть {inventoryText}");
            }
            else
            {
                Reply($"У {target.Nick} нет предметов!");
            }
        }

        [Command("dust"), Alias("пыль", "credits"), Summary("Проверить баланс"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task Dust()
        {
            if (Context.User.Dust != null)
            {
                Reply($"{BotEntry.Channel.Value} => @{Context.User.Nick} У вас {Context.User.Dust}");
                return;
            }
            Reply($"Пользователь {Context.User.Nick} не найден в списке!");
        }

        [Command("dust"), Alias("пыль", "credits"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task Dust(User targetContext)
        {
            if (targetContext.Dust != null)
            {
                Reply($"{BotEntry.Channel.Value} => У {targetContext.Nick} {targetContext.Dust}");
                return;
            }
            Reply($"Пользователь {targetContext.Nick} не найден в списке!");
        }

        [Command("dusttop"), Alias("top", "топ"), Summary("Посмотреть на топ-10 валютных магнатов"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task DustTop()
        {
            var arr = BotEntry.RedstoneDust.OrderByDescending(x => x.Value, new DustComparer());
            var arr2 = arr.Take(arr.Count() > 10 ? 10 : arr.Count()).ToList();
            string Text = "Топ владельцев пыли: ";
            for (int i = 0; i < arr2.Count(); i++)
            {
                Text += $"{i + 1}: {arr2[i].Key} => {arr2[i].Value.Dust} ";
            }

            Reply(Text);
        }

        [Command("dice"), Alias("кости"), Summary("Сыграть с ботом в кости. Поосторожнее там со ставками KappaPride"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task Dice(int di, int va)
        {
            if (va > Context.User.Dust.Dust)
            {
                Reply($"@{Context.User.Nick} Ставка не может превышать ваше количесво пыли!");
                return;
            }

            if (BotEntry.RedstoneDust.ContainsKey(Context.User.Username))
            {
                if (di > 5 && di < 37)
                {
                    var value = RNG.Next(1, 7) + RNG.Next(1, 7) + RNG.Next(1, 7) + RNG.Next(1, 7) + RNG.Next(1, 7) + RNG.Next(1, 7);
                    if (Context.User.Dust.Dust > 80)
                        if (18 > value && value < 26)
                        {
                            if (RNG.Next(0, 101) < 55)
                            {
                                if (RNG.Next(0, 101) > 45)
                                    value /= 2;
                                else value /= 3;
                                if (value < 7)
                                    value = RNG.Next(6, 10);
                            }
                        }

                    if (value == di)
                    {
                        Context.User.Dust.Dust += va * 3;
                        Reply($"Бросает кости... На костях выпадает сумма {value}... Вы угадали и выйграли {va * 3} пыли! @{Context.User.Nick} Ваш баланс теперь {Context.User.Dust}");
                        return;
                        //delay = CommandDelay;
                    }
                    else if ((value == di + 1 || value == di - 1) && Context.User.Dust.Dust < 80)
                    {
                        Context.User.Dust.Dust += va;
                        Reply($"Бросает кости... На костях выпадает сумма {value}... Вы почти угадали и казино пошло на уступку. Вы выйграли {va} пыли! @{Context.User.Nick} Ваш баланс теперь {Context.User.Dust}");
                        return;
                    }
                    else
                    {
                        Context.User.Dust.Dust -= va;
                        Reply($"Бросает кости... На костях выпадает сумма {value}... Вы проиграли {va} пыли! @{Context.User.Nick} Ваш баланс теперь {Context.User.Dust}");
                        return;
                        //delay = CommandDelay;
                    }
                }
                else
                {
                    Reply($"@{Context.User.Nick} Вне диапазона: 6 <= {di} => 36");
                    return;
                }


            }
        }

        [Command("dice"), Alias("кости"), Summary("Сыграть с ботом в кости. Поосторожнее там со ставками KappaPride"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task Dice()
        {
            Reply("!dice <число от 6 до 36> <ставка>");
        }

        [Command("dupe"), Alias("дюп"), Summary("Проверить свою удачу и скилл в залагивании серверов. За 4 факела и 2 поршня вы можете получить случайное количество пыли!"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task Dupe()
        {
            if (Context.User.Dust.GetItem<Piston>() < 1 || Context.User.Dust.GetItem<RedstoneTorch>() < 4)
            {
                string text = "";
                if (Context.User.Dust.GetItem<Piston>() < 1)
                {
                    text += "1 поршень ";
                }
                if (Context.User.Dust.GetItem<RedstoneTorch>() < 4)
                {
                    text += $"{4 - Context.User.Dust.GetItem<RedstoneTorch>().Amouth} факелов ";
                }

                Reply($"@{Context.User.Nick} У вас недостаточно предметов! Нужно ещё {text}");
                return;
            }
            else if (Context.User.Dust.GetItem<Piston>() >= 1 && Context.User.Dust.GetItem<RedstoneTorch>() >= 4)
            {
                var rnd = RNG.Next(0, 21);
                Context.User.Dust.Dust += rnd;
                Context.User.Dust.GetItem<Piston>().Amouth -= 1;
                Context.User.Dust.GetItem<RedstoneTorch>().Amouth -= 4;
                if (rnd > 0)
                    Reply($"@{Context.User.Nick} После того как сервер отвис на земле уже лежало {rnd} пыли. Теперь у вас {Context.User.Dust}");
                else
                {
                    Reply($"@{Context.User.Nick} После того как сервер отвис от машины не осталось ни следа. Пыли не прибавилось так что ваш счёт по прежнему {Context.User.Dust}");
                }
            }
        }

        [Command("addust"), HideFromHelp, Summary("Usage !addust <user> <amouth>"), RequireUserPermission(ChannelPermission.Moderator), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task AddDust(User target, int amouth)
        {
            target.Dust.Dust += amouth;
            Reply($"{target.Nick} было добавлено {amouth} пыли. Теперь у него {target.Dust}");
        }

        [Command("takedust"), HideFromHelp, Summary("Usage !takedust <user> <amouth>"), RequireUserPermission(ChannelPermission.Moderator), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task TakeDust(User target, int amouth)
        {
            target.Dust.Dust -= amouth;
            Reply($"{target.Nick} было убрано {amouth} пыли. Теперь у него {target.Dust}");
        }

        [Command("setdust"), HideFromHelp, Summary("Usage !setdust <user> <amouth>"), RequireUserPermission(ChannelPermission.Moderator), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task SetDust(User target, int amouth)
        {
            target.Dust.Dust = amouth;
            Reply($"Баланся кошелька {target.Nick} установлен на {amouth} пыли.");
        }

        [Command("giveitem"), HideFromHelp, Summary("Usage !giveitem <user> <item> [amouth]"), RequireUserPermission(ChannelPermission.Moderator), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task GiveItem(User target, Item item, int amouth = 1)
        {
            target.Dust.GetItem(item.GetType()).Amouth += amouth;
            Reply($"В инвентарь {target.Nick} было добавлено {item.GetPurchaseString(amouth)}.");
        }

        [Command("takeitem"), HideFromHelp, Summary("Usage !takeitem <user> <item> [amouth]"), RequireUserPermission(ChannelPermission.Moderator), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task TakeItem(User target, Item item, int amouth = 1)
        {
            target.Dust.GetItem(item.GetType()).Amouth -= amouth;
            if (target.Dust.GetItem(item.GetType()).Amouth < 0)
                target.Dust.GetItem(item.GetType()).Amouth = 0;
            Reply($"Из инвентаря {target.Nick} было изъято {item.GetPurchaseString(amouth)}");
        }

        [Command("givedust"), Summary("Передать кому-то пыль (Передавать могут только сабы) !givedust <user> <amouth>"), RequireUserPermission(ChannelPermission.BothAny)]
        public async Task GiveDust(User target, int amouth)
        {

            if (amouth < 0 || amouth > Context.User.Dust.Dust)
            {
                Reply($"@{Context.User.Nick} Эй, это команда передачи пыли, а не грабежа SMOrc");
                return;
            }
            target.Dust.Dust += amouth;
            Context.User.Dust.Dust -= amouth;
            Reply($"@{Context.User.Nick} передал @{target.Nick} {amouth} пыли!");
        }



        [Command("purchase"), Alias("купить"), Summary("Купить предмет в магазине бота. Использование: !purchase <предмет> [количество]"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task Purchase(Item item, int amouth = 1)
        {
            if (amouth < 0)
                return;
            if (item.Price <= 0)
            {
                Reply($"Предмет {item.GetPurchaseString(1)} не продаётся!");
            }

            if (amouth * item.Price > Context.User.Dust.Dust)
            {
                Reply($"@{Context.User.Nick} У вас недостаточно пыли что бы купить {item.GetPurchaseString(amouth)}!");
                return;
            }

            Context.User.Dust.Dust -= amouth * item.Price;

            Context.User.Dust.GetItem(item.GetType()).Amouth += amouth;

            Reply($"@{Context.User.Nick} вы купили {item.GetPurchaseString(amouth)} за {amouth * item.Price} пыли. {Context.User.Dust} осталось на счету.");

            //target.Dust.Dust = amouth;
            //ReplyAsync($"Баланся кошелька {target.Nick} установлен на {amouth} пыли.");
        }

        [Command("purchase"), Alias("купить"), Summary("Купить предмет в магазине бота. Использование: !purchase <предмет> [количество] либо просто !purchase что бы посмотреть ассортимент бота"), MainChannelException("Действия с предметами и пылью производятся в комнате #bot")]
        public async Task Purchase()
        {
            string text = "";
            foreach (var s in BotEntry.RegisteredItems)
                if (s.Price > 0)
                    text += $"{s.GetPurchaseString(1)} ({s.Price} пыли), ";
            text = text.Substring(0, text.Length - 2);
            Reply($"@{Context.User.Nick}, Для покупок доступны: {text}. !купить <товар> [сколько]");
        }

        private static DustConfig Config;

        public static DesktopStorage Storage;

        protected override void OnModuleBuilding(CommandService commandService, ModuleBuilder builder)
        {
            base.OnModuleBuilding(commandService, builder);

            Config = new DustConfig(Storage = (DesktopStorage)BotEntry.Storage.GetStorageForDirectory("Dust"));
            Config.Load();

            Colldown = Config.GetBindable<int>(DustConfigEnum.GainTime);
            SubMultiply = Config.GetBindable<int>(DustConfigEnum.SubMul);

            DustRecalculateTime = DateTimeOffset.Now.AddSeconds(Colldown);

            BotEntry.RegisterItem<RedstoneTorch>();
            BotEntry.RegisterItem<Piston>();

            BotEntry.OnTickActions += OnTick;
        }

        private static DateTimeOffset DustRecalculateTime = DateTimeOffset.Now;

        public static Bindable<int> Colldown;

        public static Bindable<int> SubMultiply;

        private void OnTick()
        {
            if (DateTimeOffset.Now > DustRecalculateTime)
            {
                var req = new WebRequest($"https://beta.decapi.me/twitch/uptime/{BotEntry.Channel.Value.Substring(1, BotEntry.Channel.Value.Length - 1)}");
                req.Finished += () =>
                {
                    var vars = req.ResponseString.Split(' ');
                    if (vars.Length >= 3 && vars[2] == "offline")
                    {
                        return;
                    }

                    var lreq = new JsonWebRequest<ChatData>($"https://tmi.twitch.tv/group/Context.User/{BotEntry.Channel.Value.Replace("#", "")}/chatters");

                    lreq.Finished += () =>
                    {
                        foreach (var it in lreq.ResponseObject.chatters.viewers)
                        {
                            if (BotEntry.RedstoneDust.ContainsKey(it))
                            {
                                if (BotEntry.RedstoneDust[it].HasBoost)
                                {
                                    BotEntry.RedstoneDust[it].Dust += 1 * SubMultiply;
                                }
                                else
                                {
                                    BotEntry.RedstoneDust[it].Dust += 1;
                                }

                            }
                            else
                            {
                                BotEntry.RedstoneDust.Add(it, new RedstoneData
                                {
                                    Dust = 1,
                                    HasBoost = false,
                                });
                            }
                            if (BotEntry.RedstoneDust[it].Chatter)
                            {
                                BotEntry.RedstoneDust[it].Dust += 1;
                                BotEntry.RedstoneDust[it].Chatter = false;
                            }
                        }
                        foreach (var it in lreq.ResponseObject.chatters.moderators)
                        {
                            if (BotEntry.RedstoneDust.ContainsKey(it))
                            {
                                if (BotEntry.RedstoneDust[it].HasBoost)
                                {
                                    BotEntry.RedstoneDust[it].Dust += 1 * SubMultiply;
                                }
                                else
                                {
                                    BotEntry.RedstoneDust[it].Dust += 1;
                                }
                            }
                            else
                            {
                                BotEntry.RedstoneDust.Add(it, new RedstoneData
                                {
                                    Dust = 1,
                                    HasBoost = false,
                                });
                            }
                            if (BotEntry.RedstoneDust[it].Chatter)
                            {
                                BotEntry.RedstoneDust[it].Dust += 1;
                                BotEntry.RedstoneDust[it].Chatter = false;
                            }
                        }


                        using (var file = new StreamWriter(Storage.GetStream($"{BotEntry.Channel}#RedstoneData.json", FileAccess.ReadWrite, FileMode.Truncate)))
                        {
                            file.Write(JsonConvert.SerializeObject(BotEntry.RedstoneDust));
                        }

                    };

                    lreq.PerformAsync();

                };

                req.PerformAsync();



                DustRecalculateTime = DateTimeOffset.Now.AddSeconds(Colldown);
            }
        }

    }
}
