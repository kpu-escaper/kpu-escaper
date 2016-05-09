// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 							ProtoBlox 1.0, Copyright © 2013, RipCord Development
//											spinAndBounce.js
//										   info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - This script controls rotation speed and direction of any object it is applied to.  It can also make the object hover up and down.


var spinRate : float;					//How fast the object will spin
var bounceAmount : float = 0.25;		//How far the object will bounce up and down
var bounceSpeed : float = 3.0;			//How fast the object will bounce up and down


function Update () {
	transform.Rotate(0, (spinRate * Time.deltaTime), 0);
	transform.Translate (0, Mathf.Sin (Time.time * bounceSpeed) * (bounceAmount / 100), 0);
}