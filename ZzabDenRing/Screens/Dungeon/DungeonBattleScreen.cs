using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Dungeon
{
    public class DungeonBattleScreen : BaseScreen
    {
        protected override void DrawContent()
        {
            DungeonBattleScreenShow();
        }

        protected override bool ManageInput()
        {
            var key = ReadKey();
            var command = key.Key switch
            {
                ConsoleKey.D1 => Command.Nothing,
                ConsoleKey.D2 => Command.Nothing,
                ConsoleKey.D3 => Command.Nothing,
                _ => Command.Nothing
            };
            return command != Command.Exit;
        }

        private List<Monster> _monsters;

        // 임의의 캐릭터 설정
        public Character _character;
        int damage = 0;


        public DungeonBattleScreen(List<Monster> monsters)
        {
            _monsters = monsters;
            // 이때 캐릭터 정보도 같이 가져오면 좋겠습니다.
            // _character = character;
        }

        private void DungeonBattleScreenShow()
        {
            BattleStartPhase();
        }

        private void BattleStartPhase()
        {
            WriteLine("Battle!!");
            WriteLine();

            for (int i = 0; i < _monsters.Count; i++)
            {
                WriteLine($"Lv. {_monsters[i].Level} {_monsters[i].Name}  HP {_monsters[i].Hp}");
            }
            WriteLine();

            // 캐릭터 정보 가져와야됨
            WriteLine("[내정보]");
            WriteLine($"Lv.(character.Lv) (character.Name) ((character.Job))");
            WriteLine($"HP (character.MaxHp)/(character.Hp)");
            WriteLine();

            WriteLine("1. 공격");
            WriteLine("2. 스킬");
            WriteLine("3. 도주");
        }

        private void BattleCharacterPhase1()
        {
            for (int i = 0; i < _monsters.Count; i++)
            {
                if (_monsters[i].Hp > 0)
                {
                    WriteLine($"{i} Lv. {_monsters[i].Level} {_monsters[i].Name}  HP {_monsters[i].Hp}");
                }
                else
                {
                    WriteLine($"{i} Lv. {_monsters[i].Level} {_monsters[i].Name}  Dead");
                }
            }

            // 캐릭터 정보 가져와야됨
            WriteLine("[내정보]");
            WriteLine($"Lv.(character.Lv) (character.Name) ((character.Job))");
            WriteLine($"HP (character.MaxHp)/(character.Hp)");
        }

        private void BattleCharacterPhase2()
        {
            // 캐릭터페이즈2에서 고른 번호-1를 가져옴
            int hpBeforeBattle = CharacterAttack(0);
            WriteLine("Battle!!");
            WriteLine();

            WriteLine($"{_character.Name} 의 공격!");
            WriteLine($"Lv.(monster[고른번호 - 1].Level) (monster[고른번호 - 1].Name) 을(를) 맞췄습니다. [데미지 : [{hpBeforeBattle} - (monster[고른번호 - 1].Hp]]");
            WriteLine();

            WriteLine($"Lv.(monster[고른번호 - 1].Level) (monster[고른번호 - 1].Name)");
            //if (_monsters[i].Hp > 0)
            WriteLine($"HP {hpBeforeBattle} -> (monster[고른번호 - 1].Hp");
            //else
            WriteLine($"HP {hpBeforeBattle} -> Dead");
            WriteLine();

            WriteLine("다음");
        }

        // hp가 0 초과인 몬스터들의 수만큼 밤복
        private void BattleMonsterPhase(int a) {
            WriteLine("Battle!!");
            WriteLine();
            //몬스터 지정필요
            int hpBeforeBattle = MonsterAttack(a);

            WriteLine($"{_monsters[a - 1].Name} 의 공격!");
            WriteLine($"Lv.{_character.Level} {_character.Name} 을(를) 맞췄습니다. [데미지 : {hpBeforeBattle} - (character.Hp)]");
            WriteLine();

            WriteLine($"Lv.{_character.Level} {_character.Name}");
            //if (_character.Hp > 0)
            WriteLine($"HP {hpBeforeBattle} -> (character.Hp_");
            //else
            WriteLine($"HP {hpBeforeBattle} -> Dead");
            WriteLine();

            WriteLine("다음");
        }

        private int CharacterAttack(int a)
        {
            // 플레이어 공격력 계산
            int attack = Random.Shared.Next((int)(_character.Atk * 0.9), (int)(_character.Atk * 1.1)) + 1;
            if (_character.IsCritical())
            {
                attack = (int)(attack + 1.6);
            }
            // 입력받았던 번호를 가져와서 출력
            int hpBeforeBattle = _monsters[a - 1].Hp;
            if (attack - _monsters[a - 1].Def > 0)
            {
                _monsters[a - 1].Hp -= attack - _monsters[a - 1].Def;
            }
            else
            {
                _monsters[a - 1].Hp -= 1;
            }
            // 계산전 체력을 리턴
            return hpBeforeBattle;
        }

        // 현재체력이 hp > 0 이상인 몬스터만 실행(반복문)
        private int MonsterAttack(int a)
        {
            int attack = Random.Shared.Next((int)(_monsters[a - 1].Atk * 0.9), (int)(_monsters[a - 1].Atk * 1.1)) + 1;
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

    }
}