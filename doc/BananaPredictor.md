# Name

BananaPredictor - An application to predict where bananas will be placed

# Synopsis

bnprdctr [-o file] [-b file] [-p x:time|...] [-d distance]

# Description

BananaPredictor is an application made to allow for the user to predict and place Banana Shower's bananas in areas which alone couldn't be done without the utilisation of Juice Streams as they help allow the bananas to change it's RNG seed to be placed in different areas. When each Banana Shower's bananas are being evaluated on their x-axis, they modify the RNG values for the rest of the map.

Unlike Banana Showers; Juice Streams are highly controlable in their placement, high count of nested objects, and ability to easily influence the RNG factor; the nested objects in which we can effect the RNG are the Droplets and TinyDroplets. The porocess in which Juice Streams follows; the new placement of the Juice Stream is to be as small as possible while reaching at least 3 nested objects which much have a Droplet or TinyDroplet. Once the Juice Stream has been placed, we evaluate the RNG and see if the Banana Shower's bananas are in the areas given by the user. If so, go to the next point to be evaluated. If not, make the Juice Stream as long as possible before it hits the BananaShower end time; if this case is hit then we start over by placing a new Juice Streams then repeat.

# Resources

## Beatmap (`-o` or `--output`) - String

Uses the output file as output all the stored objects that were identified to modify the Banana Shower's bananas.

## Beatmap (`-b` or `--beatmap`) - String

Uses the beatmap file as input for difficulty, timing point, and hit object values to reference from.

## Points (`-p` or `--points`)

The format for a point is `x:time|...`. It is mandatory that there is at least three points. The final point will always go back to the first point.

## Distance (`-d` or `--distance`)

## `x:time|...`

### x - integer

Is the x-axis for the point. It can go from 0 to 256 as that's how wide osu maps can be.

### time - integer

Is the time for the point relative to the spinner predictor's start time. If the point's time is bigger than the length of the spinner predictor, then the point's time will override it.

### :

Is a point specific seperator such that after we are done filling out one property of the point, we can fill out the next property.

### |

Is a point seperator such that after we are done filling out one point, we can fill out the next point.

# Authors

K 3 V R A L