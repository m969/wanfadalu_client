using KBEngine;
using MagicFire;
using MagicFire.Common;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;

public class NewEditorTest {

	[Test]
	public void EditorTest()
	{
		////Arrange
		//var gameObject = new GameObject();

		////Act
		////Try to rename the GameObject
		//var newGameObjectName = "My game object";
		//gameObject.name = newGameObjectName;

		////Assert
		////The object has a new name
		//Assert.AreEqual(newGameObjectName, gameObject.name);
	    var entity = Substitute.For<Entity>();
	    entity.className = "Avatar";
	}
}
