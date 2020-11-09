# <img src="samlang-logo.png" alt="Sam-Lang Logo" width="5%"/> SamLang Manual

## NOTICE
SamLang does *not* support the MIV app! (The new micro *isn't tested, either!*)

## SamLang Commands
* print - same as echo.
* print-nlb - print/echo without linebreaks.
* beep - beeps.
* math+ - addition of two numbers, for example: "math+ 2 3" would output 5.
* math* - like math+ but multiplies, for example "math* 2 3" would output 6.
* math/ - like math* but divides, for example "math/ 6 3" would output 2.
* math- - like math/ but subtracts, for example "math- 6 3" would output 3.
* beep-custom - for example, "beep-custom 1500 1000" would output a 1 second beep at 1500hz

## Instructions for writing a "Hello, GEMS and SamLang!" app.
* create a new file using mkfile.
* 2file print Hello, GEMS
    * All of these 2file commands in this tutorial uses the file we created.
* 2file print-nlb and SamLang!
* 2file beep
* Run using: samlang *filename*

Results should be an echo with "Hello, GEMS" with a linebreak after it, an echo without linebreaks with the text "and Samlang!", and a beep!
