using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingGenerator
{
    public static Building Generate(BuildingSettings settings)
    {
        return new Building(settings.Size.x, settings.Size.y, GenerateWings(settings));
    }

    static Wing[] GenerateWings(BuildingSettings settings)
    {
        return new Wing[] { GenerateWing(settings, new RectInt(0, 0, settings.Size.x, settings.Size.y), 1) };
    }

    static Wing GenerateWing(BuildingSettings settings, RectInt bounds, int numberOfStories)
    {
        return new Wing(
            bounds,
            GenerateStories(settings, bounds, numberOfStories),
            GenerateRoof(settings, bounds));
    }

    static Story[] GenerateStories(BuildingSettings settings, RectInt bounds, int numberOfStories)
    {
        return new Story[] { GenerateStory(settings, bounds, 1) };
    }

    static Story GenerateStory(BuildingSettings settings, RectInt bounds, int level)
    {
        return new Story(0, GenerateWalls(settings, bounds, level));
    }

    static Wall[] GenerateWalls(BuildingSettings settings, RectInt bounds, int level)
    {
        return new Wall[(bounds.size.x + bounds.size.y) * 2];
    }

    static Roof GenerateRoof(BuildingSettings settings, RectInt bounds)
    {
        return new Roof();
    }
}
