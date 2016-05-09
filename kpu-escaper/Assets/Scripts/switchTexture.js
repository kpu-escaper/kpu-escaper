// /-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\
//
// 							ProtoBlox 1.0, Copyright 짤 2013, RipCord Development
//											 switchTexture.js
//										    info@ripcorddev.com
//
// \-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/-\-/

//ABOUT - This script selects all the objects in the scene with a specific tag and then switches their texture to the next one in the array.


var textures : Material[];
private var t : int = 1;


function OnMouseUp () {

	var demoObjects : GameObject[] = GameObject.FindGameObjectsWithTag("demo");
	
	for (var x = 0; x < demoObjects.Length; x++) {
		demoObjects[x].GetComponent.<Renderer>().material = textures[t];
	}
	if (t < textures.Length - 1) {
		t++;
	}
	else {
		t = 0;
	}

}