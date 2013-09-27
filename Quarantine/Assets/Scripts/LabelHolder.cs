using UnityEngine;
using System.Collections;

public class LabelHolder : Object {
	public string label;
	public Interface.labelIndices indice;
	
	public LabelHolder(string label, Interface.labelIndices indice){
		this.label = label;
		this.indice = indice;
	}
}
