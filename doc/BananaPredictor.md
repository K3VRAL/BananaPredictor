# Name

BananaPredictor - An application to predict where bananas will be placed

# Synopsis

bnprdctr [-b file] [-o [file]] [-O [file]] [-s x:time|x:time|x:time[|...]] [-j [[f|][l=len|]x:y|x:y[|...]] [-d [distance]] [-p] [-r] [-h]

# Description

BananaPredictor is an application made to allow the user to predict and place Banana Shower's bananas in areas which alone couldn't be done without the utilisation of Juice Streams as they help allow the bananas to change their RNG seed to be placed in different areas. When each Banana Shower's bananas are being evaluated on their x-axis, they modify the RNG values for the rest of the map.

Unlike Banana Showers; Juice Streams are highly controllable in their placement, high count of nested objects, and ability to easily influence the RNG factor; the nested objects in which we can affect the RNG are the Droplets and TinyDroplets. The process in which Juice Streams follows; the new placement of the Juice Stream is to be as small as possible while reaching at least 3 nested objects which much have a Droplet or TinyDroplet. Once the Juice Stream has been placed, we evaluate the RNG and see if the Banana Shower's bananas are in the areas given by the user. If so, go to the next point to be evaluated. If not, make the Juice Stream as long as possible before it hits the BananaShower end time; if this case is hit then we start over by placing a new Juice Stream, then repeat.

# Resources

## Beatmap (`-b` or `--beatmap`) - File Location

Uses the beatmap file as input for difficulty, timing point, and hit object values to reference from.

If the argument is not used, it will default to `NULL` and prevent the application from running.

## Output (`-o` or `--output`) - File Location

Uses the output file as output for all the stored objects that were identified to modify the Banana Shower's bananas.

If the argument is not used, it will default to the terminal's output.

## Output Beatmap (`-O` or `--output-beatmap`) - File Location

Uses the output file as output for all the stored objects that were identified to modify the Banana Shower's bananas with the osu! format. Record Objects (`-r` or `--record-objects`) will default to `true`. 

If the argument is not used, it will default to the terminal's output.

## Shapes (`-s` or `--shapes`) - Formatted as `int:int|int:int|int:int[|...]`

The format for a point is `x:time|x:time|x:time[|...]`. There must be at least three points. The final point will always go back to the first point.

If the argument is not used, it will default to `NULL` and prevent the application from running.

You also can stack the argument multiple times, allowing you to have multiple shapes; each shape works independently.

## Juice Points (`-j` or `--juice-points`) - Formatted as `[f|][l=int|]int:int|int:int[|...]`

The format for a point is `[f|][l=len|]x:y|x:y[|...]`. There must be at least two points. The first point will always be the x and y coordinates for Juice Stream whereas the rest will be its curve.

If the argument is not used, it will default to `NULL` and the default format will be used instead.

You also can stack the argument multiple times, allowing you to have multiple Juice Streams; each Juice Stream works dependently on each other and the ordering of each Juice Stream placed matters.

If the `f` item (Boolean as argument) is used, it means that the Juice Stream blueprint will be followed on each new Juice Stream placement.

If the `l` item (Integer as argument) is used, it means that the Juice Stream length is changed to the new value provided. If this is not used, it will be defaulted to the end time of the BananaPredictor.

## Distance (`-d` or `--distance`) - Double

Allows for the distance for each Banana Shower. Since Banana Showers and Juice Streams require a lot of loads, this can be reduced by spreading them out.

If the argument is not used, it will default to `1`.

The value does not have to only be an interger as it can also be expressed as a float.

## Prefer Circles (`-p` or `--prefer-circles`) - Boolean

Outputs the given Banana Shower's bananas to circles (ignores Juice Streams). This can be used for debugging if the application is not giving the proper output; whether it be an error on the user's part or an error on the application's part.

If the argument is not used, it will default to `false`.

## Record Objects (`-r` or `--record-objects`) - Boolean

Outputs and includes input map objects.

If the argument is not used, it will default to `false`.

If the Output Beatmap (`-O` or `--output-beatmap`) argument is used, it will default to `true`. If this argument is then later used; it will not record the beatmap.

## Help (`-h` or `--help`) - Boolean

Gives all the commands to the terminal's output

## Points format

### f - Boolean

This is used if the Juice Streams generated should follow the master given its current time.

### x - Integer

It is the x-axis for the point. It can go from 0 to 256 as that's how wide osu! maps can be.

### time or y - Integer

It is the time for the point relative to the spinner predictor's start time. If the point's time is bigger than the length of the spinner predictor, then the point's time will override it.

### : - Character

It is a point-specific separator such that after we are done filling out one property of the point, we can fill out the next property.

### | - Character

It is a point separator such that after we are done filling out one point, we can fill out the next point.

# Authors

K 3 V R A L