# Bullet-Hell-Shooter

This repo contains **Figure Hell Shooter**, a Bullet Hell Shooter created for the _tc2008b_ course in modeling of multi-agent systems with computer graphics.

The theme chosen for this game is **Figures**. That's the reason all the sprites are related with geometry and lines _(and because those sprites where the easiest to make)_.

The level of difficulty chosen for this project is: **Lunático**

Link to the [video](https://youtu.be/3inYzeLOsjg?feature=shared).

## References

ChatGPT was used, but not to help me writing methods or giving me ideas for logic. ChatGPT was used ONLY in:
 - [VectorManager](/Game/Assets/Scripts/Utils/VectorManager.cs) to help me debug. I was trying to rotate vectors, but I had no idea the methods I was using worked with radians insted of degrees. Chat only helped me debbuging.
 - [BaseEnemyScript](/Game/Assets/Scripts/Characters/Enemies/BaseEnemyScript.cs) to help me debug. I was trying to use the method `OnTriggerEnter()`, but it didn't work because I'm working on a 2D game. Chat told me that I needed to use `OnTriggerEnter2D()` for the method to work properly.
 - [BulletManager](/Game/Assets/Scripts/Bullets/BulletManager.cs) to help me initializing arrays. Prior to this I had pretty much no experience in C#, and when I tried initializing an array it threw me errors. Chat only told me how to use them in C#
 - The creation of math formulas to eval them using Ncalc. It thought it was faster to just prompt for math formulas that could create interesting patterns.

In summary, ChatGPT didn't help me on creating methods or giving me ideas, it only helped me to debug and to find the best math formulas.

I also based on a Youtube video on how to create an [object pool](https://www.youtube.com/watch?v=3IJg8T-E68s).