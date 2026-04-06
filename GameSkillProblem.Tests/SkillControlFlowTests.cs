using Xunit;

namespace GameSkillProblem.Tests
{
    public class SkillControlFlowTests
    {
        [Fact]
        public void C2_01()
        {
            // P1: Character isDead() = TRUE → Returns FALSE
            // Caster: HP=0, Mana=100 | Skill: cooldown=0, mana=50, damage=10 | Target: null
            var skill = new Skill(0, 50, 10, "Fireball");
            var caster = new Character("Hero", 0, 100);
            
            var result = skill.Cast(caster, null);
            
            Assert.False(result);
        }

        [Fact]
        public void C2_02()
        {
            // P2: isAlive=TRUE, CurrentCooldown > 0 → Returns FALSE
            // Caster: HP=100, Mana=100 | Skill: cooldown=10, currentCooldown=5, mana=50, damage=10 | Target: HP=100
            var skill = new Skill(10, 50, 10, "Fireball");
            skill.SetCurrentCooldown(5);
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);
            
            var result = skill.Cast(caster, target);
            
            Assert.False(result);
        }

        [Fact]
        public void C2_03()
        {
            // P3: isAlive=TRUE, Cooldown=0, not enough mana → Returns FALSE
            // Caster: HP=100, Mana=30 | Skill: cooldown=0, mana=50, damage=10 | Target: HP=100
            var skill = new Skill(10, 50, 10, "Fireball");
            var caster = new Character("Hero", 100, 30);
            var target = new Character("Enemy", 100, 50);
            
            var result = skill.Cast(caster, target);
            
            Assert.False(result);
        }

        [Fact]
        public void C2_04()
        {
            // P4: skill.Target required but target = null → Returns FALSE
            // Caster: HP=100, Mana=100 | Skill: cooldown=0, mana=50, damage=10, Target="Enemy" | Input target: null
            var skill = new Skill(10, 50, 10, "Enemy");
            var caster = new Character("Hero", 100, 100);
            
            var result = skill.Cast(caster, null);
            
            Assert.False(result);
        }

        [Fact]
        public void C2_05()
        {
            // P5: target isDead → Returns FALSE
            // Caster: HP=100, Mana=100 | Skill: cooldown=0, mana=50, damage=10 | Target: HP=0
            var skill = new Skill(10, 50, 10, "Fireball");
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 0, 50);
            
            var result = skill.Cast(caster, target);
            
            Assert.False(result);
        }

        [Fact]
        public void C2_06()
        {
            // P6: Cast success + Damage > 0 → TRUE + deals damage
            // Caster: HP=100, Mana=100 | Skill: cooldown=0, mana=50, damage=30 | Target: HP=100
            var skill = new Skill(10, 50, 30, "Fireball");
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);
            
            var result = skill.Cast(caster, target);
            
            Assert.True(result);
            Assert.Equal(70, target.CurrentHP);
        }

        [Fact]
        public void C2_07()
        {
            // P7: Cast success + Damage = 0 → TRUE + no damage
            // Caster: HP=100, Mana=100 | Skill: cooldown=0, mana=50, damage=0 | Target: HP=100
            var skill = new Skill(10, 50, 0, "Heal");
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Ally", 100, 50);
            
            var result = skill.Cast(caster, target);
            
            Assert.True(result);
            Assert.Equal(100, target.CurrentHP);
        }
    }
}
