# Name

BananaPredictor - An application to predict where bananas will be placed

# Synopsis

bnprdctr [-o file] [-b file] [-p x:time|...] [-d distance] [--prefer-circle]

# Description

BananaPredictor is an application made to allow for the user to predict and place Banana Shower's bananas in areas which alone couldn't be done without the utilisation of Juice Streams as they help allow the bananas to change it's RNG seed to be placed in different areas. When each Banana Shower's bananas are being evaluated on their x-axis, they modify the RNG values for the rest of the map.

Unlike Banana Showers; Juice Streams are highly controlable in their placement, high count of nested objects, and ability to easily influence the RNG factor; the nested objects in which we can effect the RNG are the Droplets and TinyDroplets. The porocess in which Juice Streams follows; the new placement of the Juice Stream is to be as small as possible while reaching at least 3 nested objects which much have a Droplet or TinyDroplet. Once the Juice Stream has been placed, we evaluate the RNG and see if the Banana Shower's bananas are in the areas given by the user. If so, go to the next point to be evaluated. If not, make the Juice Stream as long as possible before it hits the BananaShower end time; if this case is hit then we start over by placing a new Juice Streams then repeat.

# Resources

## Output (`-o` or `--output`) - String (file location)

Uses the output file as output all the stored objects that were identified to modify the Banana Shower's bananas.

If argument is not used, it will be defaulted to the terminal's output.

## Prefer Circles (`--prefer-circles`) - Boolean

Outputs the given Banana Shower's bananas to circles (ignores Juice Streams). This can be used for debugging if the application is not giving the proper output; whether it be an error on the user's part or an error on the application's part.

If argument is not used, it will be defaulted to `false`.

## Beatmap (`-b` or `--beatmap`) - String (file location)

Uses the beatmap file as input for difficulty, timing point, and hit object values to reference from.

If argument is not used, it will be defaulted to `NULL` and prevent the application from running.

## Distance (`-d` or `--distance`) - Unsigned Integer

Allows for the distance for each Banana Shower. Since Banana Showers and Juice Streams requires a lot of load, this can be reduced by spreading the them out.

If argument is not used, it will be defaulted to `1`.

## Points (`-p` or `--points`) - String (formatted as `x:time|...`)

The format for a point is `x:time|...`. It is mandatory that there is at least three points. The final point will always go back to the first point.

If argument is not used, it will be defaulted to `NULL` and prevent the application from running.

### x - Integer

Is the x-axis for the point. It can go from 0 to 256 as that's how wide osu maps can be.

### time - Integer

Is the time for the point relative to the spinner predictor's start time. If the point's time is bigger than the length of the spinner predictor, then the point's time will override it.

### :

Is a point specific seperator such that after we are done filling out one property of the point, we can fill out the next property.

### |

Is a point seperator such that after we are done filling out one point, we can fill out the next point.

## Help (`-h` or `--help`) - Boolean

Gives all the commands to the terminal's output

# Authors

K 3 V R A L