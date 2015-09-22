# Let's Build a world!

The goal of this project is to create as big of a generative world as possible, and make it super easy to add new features to the landscape.

When writing code for this project, think like a function. You can't know anything about anything that has been placed in the world already. You should be able to generate any peice of the land at any time without generating anything near it. And the world should always be generated the same given the same input seed (don't use `Math.Random` ever).

Below is a big checklist of the Different features and explanations of how to use them.

## Terrain
A World needs shape, the way we bring it to life is via a density function.

We have a master object for density, called a DensityProvider. When you need the surface density at a certain point, ask the provider to give it to you. When we render the world, we create voxels from sets of 8 density samples, and produce geometry via the marching cubes algorithm.
