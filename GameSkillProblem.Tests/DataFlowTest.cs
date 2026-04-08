using Xunit;

namespace GameSkillProblem.Tests
{
    public class DataFlowTest
    {
        [Fact]
        public void P1()
        {
            var caster = new Character("Hero", 0, 100);
            Character? target = null;
            var skill = new Skill(0, 50, 10, null, "Fireball");

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void P2()
        {
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);
            var skill = new Skill(0, 50, 0, null, "Heal");

            var manaBeforeCast = caster.CurrentMana;
            var targetHpBeforeCast = target.CurrentHP;

            var result = skill.Cast(caster, target);

            Assert.True(result);
            Assert.Equal(manaBeforeCast - skill.Mana, caster.CurrentMana);
            Assert.Equal(targetHpBeforeCast, target.CurrentHP);
            Assert.Equal(skill.Cooldown, skill.CurrentCooldown);
        }

        [Fact]
        public void P3()
        {
            var caster = new Character("Hero", 100, 30);
            var target = new Character("Enemy", 100, 50);
            var skill = new Skill(0, 50, 10, null, "Fireball");

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void P4()
        {
            var caster = new Character("Hero", 100, 100);
            Character? target = null;
            var skill = new Skill(0, 50, 10, "enemy", "Fireball");

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void P5()
        {
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);
            var skill = new Skill(10, 50, 10, null, "Fireball");
            skill.SetCurrentCooldown(5);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void P6()
        {
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 0, 50);
            var skill = new Skill(0, 50, 10, null, "Fireball");

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void P7()
        {
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);
            var skill = new Skill(0, 50, 30, null, "Fireball");

            var manaBeforeCast = caster.CurrentMana;
            var targetHpBeforeCast = target.CurrentHP;

            var result = skill.Cast(caster, target);

            Assert.True(result);
            Assert.Equal(manaBeforeCast - skill.Mana, caster.CurrentMana);
            Assert.Equal(targetHpBeforeCast - skill.Damage, target.CurrentHP);
            Assert.Equal(skill.Cooldown, skill.CurrentCooldown);
        }
    }
}
