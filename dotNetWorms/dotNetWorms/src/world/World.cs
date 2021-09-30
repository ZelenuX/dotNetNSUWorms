using System;
using System.Collections.Generic;
using System.Text;
using Utils.Containers;
using Utils.Generators;
using Services;

namespace World
{
    class World
    {
        private static int INIT_WORM_HEALTH = WorldProperties.INIT_WORM_HEALTH;
        private static int INIT_FOOD_FRESHNESS = WorldProperties.INIT_FOOD_FRESHNESS;
        private static int FOOD_HEALING_EFFECT = WorldProperties.FOOD_HEALING_EFFECT;

        private AbstractStorage2d<Worm> wormsOnField = new Storage2dInfinite<Worm>();
        private AbstractStorage2d<Food> foodOnField = new Storage2dInfinite<Food>();
        private StringBuilder stringBuilder = new StringBuilder();
        private NormalCoordsGenerator coordsGenerator;
        private UniqueNamesGenerator nameGenerator;
        private WormStrategyProviderService wormStrategyProvider;
        private void addNewFoodAndDeleteIfSpoiled()
        {
            foodOnField.ForEach((x, y, food) =>
            {
                food.DecFreshness();
                if (!food.IsFresh())
                {
                    foodOnField.Remove(x, y);
                }
            });
            Food newFood = new Food(INIT_FOOD_FRESHNESS);
            while (!foodOnField.TrySet(coordsGenerator.NextCoords(), newFood));
        }
        private void processWormTurns()
        {
            wormsOnField.ForEach((x, y, worm) => worm
                .GetNextTurn(new WormAndWorldData(x, y, wormsOnField, foodOnField))
                .Perform(x, y, wormsOnField, foodOnField));
        }
        private void feedWorms()
        {
            wormsOnField.ForEach((x, y, worm) =>
            {
                if (foodOnField.Remove(x, y))
                {
                    worm.Health += FOOD_HEALING_EFFECT;
                }
            });
        }
        private void deleteDeadWorms()
        {
            wormsOnField.ForEach((x, y, worm) =>
            {
                --worm.Health;
                if (worm.Health <= 0)
                {
                    wormsOnField.Remove(x, y);
                }
            });
        }

        public World(NormalCoordsGenerator coordsGenerator, UniqueNamesGenerator nameGenerator, WormStrategyProviderService wormStrategyProvider)
        {
            this.coordsGenerator = coordsGenerator;
            this.nameGenerator = nameGenerator;
            this.wormStrategyProvider = wormStrategyProvider;
        }

        public void nextTurn()
        {
            addNewFoodAndDeleteIfSpoiled();
            feedWorms();
            processWormTurns();
            feedWorms();
            deleteDeadWorms();
        }

        public bool TryAddWorm(int x, int y)
        {
            return wormsOnField.TrySet(x, y, new Worm(nameGenerator, INIT_WORM_HEALTH, wormStrategyProvider));
        }

        override public string ToString()
        {
            stringBuilder.Clear();
            stringBuilder.Append("Worms:[");
            if (wormsOnField.Count > 0)
            {
                wormsOnField.ForEach((x, y, worm)
                    => stringBuilder.Append(worm.Name).Append("(").Append(x).Append(",").Append(y).Append("),"));
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            stringBuilder.Append("],Food:[");
            if (foodOnField.Count > 0)
            {
                foodOnField.ForEach((x, y, food)
                    => stringBuilder.Append("(").Append(x).Append(",").Append(y).Append("),"));
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}
