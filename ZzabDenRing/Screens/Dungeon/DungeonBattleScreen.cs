﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Data;
using ZzabDenRing.Di;
using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Dungeon
{
    public class DungeonBattleScreen : BaseScreen
    {
        public const string ArgMonster = "arg_monster";
        private List<Monster> _monsters;

        public Character _character;

        private Action<Reward> _navToReward;
        private Action _navToMain;

        private bool _continueOnThisScreen = true;

        private enum BattlePhase
        {
            StartPhase,
            CharacterSelectPhase,
            CharacterAttackPhase1,
            CharacterAttackPhase2,
            CharacterAttackPhase3,
            MonsterAttackPhase,
        }

        private BattlePhase _currentPhase = BattlePhase.StartPhase;

        protected override void DrawContent()
        {
            switch (_currentPhase)
            {
                case BattlePhase.StartPhase:
                    DungeonBattleScreenShow();
                    break;
                case BattlePhase.CharacterSelectPhase:
                    BattleCharacterPhase1();
                    break;
                case BattlePhase.CharacterAttackPhase1:
                    BattleCharacterPhase2(0);
                    break;
                case BattlePhase.CharacterAttackPhase2:
                    BattleCharacterPhase2(1);
                    break;
                case BattlePhase.CharacterAttackPhase3:
                    BattleCharacterPhase2(2);
                    break;
                case BattlePhase.MonsterAttackPhase:
                    BattleMonsterPhase();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override bool ManageInput()
        {
            var key = ReadKey();
            var command = Command.Nothing;
            switch (_currentPhase)
            {
                case BattlePhase.StartPhase:
                    command = key.Key switch
                    {
                        ConsoleKey.D1 => Command.Attack,
                        // 구현 못함
                        // ConsoleKey.D2 => Command.Nothing,
                        ConsoleKey.D2 => Command.Run,
                        _ => Command.Nothing
                    };
                    break;
                case BattlePhase.CharacterSelectPhase:
                    command = key.Key switch
                    {
                        ConsoleKey.D1 => Command.AttackMonster1,
                        ConsoleKey.D2 => Command.AttackMonster2,
                        ConsoleKey.D3 => Command.AttackMonster3,
                        ConsoleKey.X => Command.BattleStart,
                        _ => Command.Nothing
                    };
                    break;
                case BattlePhase.CharacterAttackPhase1:
                    command = key.Key switch
                    {
                        ConsoleKey.Enter => Command.AttackCharacter,
                        ConsoleKey.X => Command.Attack,
                        _ => Command.Nothing
                    };
                    break;
                case BattlePhase.CharacterAttackPhase2:
                    command = key.Key switch
                    {
                        ConsoleKey.Enter => Command.AttackCharacter,
                        ConsoleKey.X => Command.Attack,
                        _ => Command.Nothing
                    };
                    break;
                case BattlePhase.CharacterAttackPhase3:
                    command = key.Key switch
                    {
                        ConsoleKey.Enter => Command.AttackCharacter,
                        ConsoleKey.X => Command.Attack,
                        _ => Command.Nothing
                    };
                    break;
                case BattlePhase.MonsterAttackPhase:
                    command = key.Key switch
                    {
                        ConsoleKey.Enter => Command.BattleStart,
                        _ => Command.Nothing
                    };
                    break;
            }

            switch (command)
            {
                case Command.Attack:
                    _currentPhase = BattlePhase.CharacterSelectPhase;
                    break;
                case Command.Run:
                    _navToMain();
                    break;
                case Command.AttackMonster1:
                    _currentPhase = BattlePhase.CharacterAttackPhase1;
                    break;
                case Command.AttackMonster2:
                    _currentPhase = BattlePhase.CharacterAttackPhase2;
                    break;
                case Command.AttackMonster3:
                    _currentPhase = BattlePhase.CharacterAttackPhase3;
                    break;
                case Command.SelectMonster:
                    _currentPhase = BattlePhase.CharacterSelectPhase;
                    break;
                case Command.AttackCharacter:
                    _currentPhase = BattlePhase.MonsterAttackPhase;
                    break;
                case Command.BattleStart:
                    _currentPhase = BattlePhase.StartPhase;
                    break;
            }

            if (!_continueOnThisScreen)
            {
                return false;
            }

            return command != Command.Run;
        }

        public DungeonBattleScreen(List<Monster> monsters, Action navToMain, Action<Reward> navToReward)
        {
            _character = Container.GetRepository().Character!;
            _monsters = monsters;
            _navToMain = navToMain;
            _navToReward = navToReward;
        }

        private void DungeonBattleScreenShow()
        {
            BattleStartPhase();
        }

        private void BattleStartPhase()
        {
            BattleLetter();

            ForegroundColor = ConsoleColor.Blue;
            WriteLine(" 행동페이즈");
            WriteLine();
            ResetColor();

            for (int i = 0; i < _monsters.Count; i++)
            {
                if (_monsters[i].Hp > 0)
                {
                    WriteLine(
                        $" Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].MaxHp}/{_monsters[i].Hp}");
                }
                else
                {
                    ForegroundColor = ConsoleColor.Gray;
                    WriteLine($" Lv.{_monsters[i].Level} {_monsters[i].Name} Dead");
                    ResetColor();
                }
            }

            WriteLine();

            // 캐릭터 정보 가져와야됨
            WriteLine(" [내정보]");
            WriteLine($" Lv.{_character.Level} {_character.Name} ({_character.Job})");
            WriteLine($" HP {_character.Hp}/{_character.MaxHp}");
            WriteLine();

            WriteLine(" 1. 공격");
            // 미구현
            // WriteLine("2. 스킬");
            WriteLine(" 2. 도주");
        }

        private void BattleCharacterPhase1()
        {
            BattleLetter();

            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine(" 공격페이즈");
            WriteLine();
            ResetColor();

            for (int i = 0; i < _monsters.Count; i++)
            {
                if (_monsters[i].Hp > 0)
                {
                    WriteLine(
                        $" {i + 1}. Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].MaxHp}/{_monsters[i].Hp}");
                }
                else
                {
                    ForegroundColor = ConsoleColor.Gray;
                    WriteLine($" {i + 1}. Lv.{_monsters[i].Level} {_monsters[i].Name} Dead");
                    ResetColor();
                }
            }

            WriteLine();

            WriteLine(" [내정보]");
            WriteLine($" Lv.{_character.Level} {_character.Name} ({_character.Job})");
            WriteLine($" HP {_character.Hp}/{_character.MaxHp}");
            WriteLine();

            WriteLine(" X. 행동 다시 고르기");
        }

        private void BattleCharacterPhase2(int index)
        {
            BattleLetter();

            if (index >= _monsters.Count || _monsters[index].Hp < 1)
            {
                WriteLine(" 지정할 수 없는 대상입니다. 돌아가십시오.");
                WriteLine();

                WriteLine(" X. 대상 다시 고르기");

                return;
            }

            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine(" 플레이어 공격");
            WriteLine();
            ResetColor();

            int hpBeforeBattle = CharacterAttack(index);

            WriteLine($" {_character.Name} 의 공격!");
            WriteLine(
                $" Lv.{_monsters[index].Level} {_monsters[index].Name} 을(를) 맞췄습니다. [데미지 : {hpBeforeBattle - _monsters[index].Hp}]");
            WriteLine();

            WriteLine($" Lv.{_monsters[index].Level} {_monsters[index].Name}");
            if (_monsters[index].Hp > 0)
            {
                WriteLine($" HP {hpBeforeBattle} -> {_monsters[index].Hp}");
            }
            else
            {
                WriteLine($" HP {hpBeforeBattle} -> Dead");
            }

            WriteLine();

            WriteLine(" Enter. 다음");
            _continueOnThisScreen = _monsters.Any(monster => monster.Hp > 0);

            //todo 보상 item 추가
            if (!_continueOnThisScreen)
            {
                var gold = _monsters.Sum(it => it.RewardGold);
                _navToReward(new Reward(gold, Array.Empty<IItem>()));
            }
        }

        // hp가 0 초과인 몬스터들의 수만큼 밤복
        private void BattleMonsterPhase()
        {
            BattleLetter();

            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(" 몬스터 공격");
            WriteLine();
            ResetColor();

            for (int i = 0; i < _monsters.Count; i++)
            {
                if (_monsters[i].Hp < 1)
                {
                    continue;
                }

                int hpBeforeBattle = MonsterAttack(i);

                WriteLine($" {_monsters[i].Name} 의 공격!");
                WriteLine(
                    $" Lv.{_character.Level} {_character.Name} 을(를) 맞췄습니다. [데미지 : {hpBeforeBattle - _character.Hp}]");
                WriteLine();

                WriteLine($" Lv.{_character.Level} {_character.Name}");
                if (_character.Hp > 0)
                {
                    WriteLine($" HP {hpBeforeBattle} -> {_character.Hp}");
                    WriteLine();
                }
                else
                {
                    WriteLine($" HP {hpBeforeBattle} -> Dead");
                    WriteLine();

                    WriteLine(" Enter. 다음");
                    _continueOnThisScreen = false;
                    _navToMain();
                    return;
                }
            }

            WriteLine();
            WriteLine(" Enter. 다음");
        }

        private int CharacterAttack(int index)
        {
            // 플레이어 공격력 계산
            int attack = Random.Shared.Next((int)(_character.Atk * 0.9), (int)(_character.Atk * 1.1)) + 1;
            if (_character.IsCritical())
            {
                attack = (int)(attack + 1.6);
            }

            // 입력받았던 번호를 가져와서 출력
            int hpBeforeBattle = _monsters[index].Hp;
            if (attack - _monsters[index].Def > 0)
            {
                _monsters[index].Hp -= attack - _monsters[index].Def;
            }
            else
            {
                _monsters[index].Hp -= 1;
            }

            // 계산전 체력을 리턴
            return hpBeforeBattle;
        }


        private int MonsterAttack(int index)
        {
            int attack = Random.Shared.Next((int)(_monsters[index].Atk * 0.9), (int)(_monsters[index].Atk * 1.1)) + 1;
            int hpBeforeBattle = _character.Hp;
            if (attack - _character.Def > 0)
            {
                _character.Hp -= attack - _character.Def;
            }
            else
            {
                _character.Hp -= 1;
            }

            // 계산전 체력을 리턴
            return hpBeforeBattle;
        }

        private void BattleLetter()
        {
            Clear();
            ForegroundColor = ConsoleColor.Red;
            WriteLine(
                "\r\n  ____        _   _   _      _\r\n |  _ \\      | | | | | |    | |\r\n | |_) | __ _| |_| |_| | ___| |\r\n |  _ < / _` | __| __| |/ _ \\ |\r\n | |_) | (_| | |_| |_| |  __/_|\r\n |____/ \\__,_|\\__|\\__|_|\\___(_)\r\n                                                                      \r\n");
            ResetColor();
            WriteLine();
        }
    }
}