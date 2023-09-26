using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*

This is a simple piece of todo software created by Will Stallwood of Cipher Prime.
http://www.cipherprime.com

*/

public class ListData : ScriptableObject
{
	public List<ListItemOwner> owners = new List<ListItemOwner>();
	public List<ListItem> items = new List<ListItem>();
	
	public ListData ()
	{
		// create over list owners, can be an editor window later		
		owners.Add( new ListItemOwner("Normal", Color.white,0) );		
		owners.Add( new ListItemOwner("Urgent", Color.red,1) );
		owners.Add( new ListItemOwner("In Progress", Color.cyan,2) );
		owners.Add( new ListItemOwner("Note", Color.yellow,3) );								
	}
	
	public void AddTask( ListItemOwner owner, string task)
	{
		ListItem item = new ListItem( owner, task );
		items.Add(item);
	}
}

[Serializable]
public class ListItem
{
	public ListItemOwner owner;
	public string task;
	public bool isComplete;
	
	public ListItem( ListItemOwner owner, string task )
	{
		this.owner = owner;
		this.task = task;
		this.isComplete = false;
	}
}

[Serializable]
public class ListItemOwner
{
	public string name;
	public Color color;
	public int index;
	
	public ListItemOwner( string name, Color color , int index)
	{
		this.name = name;
		this.color = color;
		this.index = index;
	}
}
