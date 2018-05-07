using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ControlsTest {

    //Unit test asserting that the NESW variable exists before
    //running the MobileChangeDir method
	[UnityTest]
	public IEnumerator NESWAssigned(){
		var controller = new GameController();

		Assert.NotNull (controller.NESW);
		yield return null;
	}
    
    //Unit test asserting that the NESW is updated by the MobileChangeDir
    //method if the direction is allowed
    
    [UnityTest]
	public IEnumerator NESWMatchesDirectionOptions(){

		var controller = new GameController();
		controller.MobileChangeDir (1);

		Assert.AreEqual (1, controller.NESW);
		yield return null;
	}

	//Attempt at writing a test for the Movement method, but as there is no
	//getters or setters and the method is private, it does not work.
//	[UnityTest]
//	public IEnumerator TestMovement(){
//		var controller = new GameController();
//		Vector2 orgPos = new Vector2();
//		var orgPos = controller.newPos;
//		controller.MobileChangeDir (1);
//		controller.Movement();
//		Assert.AreEqual(orgPos, controller.newPos);
//
//		yield return null;
//	}
}
