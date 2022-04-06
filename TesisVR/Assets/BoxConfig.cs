using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoxConfig {
    private static List<Vector2> list;

    public static void Init() {
        list = new List<Vector2>();
        for (int i = 2 ; i >= -2 ; i--)
        {
            for (int j = 2 ; j >= -2 ; j--)
            {
                list.Add(new Vector2(i, j));
            }
        }
    }

    /* Box number = List Index + 1
    (01)[2, 2]  (02)[1, 2]  (03)[0, 2]  (04)[-1, 2]  (05)[-2, 2]
	(06)[2, 1]  (07)[1, 1]  (08)[0, 1]  (09)[-1, 1]  (10)[-2, 1]
	(11)[2, 0]  (12)[1, 0]  (13)[0, 0]  (14)[-1, 0]  (15)[-2, 0]
	(16)[2, -1] (17)[1, -1] (18)[0, -1] (19)[-1, -1] (20)[-2, -1]
	(21)[2, -2] (22)[1, -2] (23)[0, -2] (24)[-1, -2] (25)[-2, -2]
    */

    static Vector2 IndexToCoor(int index) {
        if (index > 0 && index < 26) {
            return list[index - 1]; 
        }      
        return new Vector2(-10f, -10f);
    }

    static int CoorToIndex(Vector2 coor){
        if (list.Contains(coor)) {
            return list.IndexOf(coor) + 1;
        }
        return -1;
    }

    static Vector2 rotateClockwise(Vector2 v) {        
        return new Vector2(-v.y, v.x);
    }

    static Vector2 flip(Vector2 v) {        
        return new Vector2(-v.x, -v.y);
    }

    static Vector2 rotateCounterClockwise(Vector2 v) {
        return new Vector2(v.y, -v.x);
    }

    public static int rotateClockwise(int i) {
        Vector2 v = IndexToCoor(i);
        return CoorToIndex(rotateClockwise(v));
    }

    public static int flip(int i) {
        Vector2 v = IndexToCoor(i);
        return CoorToIndex(flip(v));
    }

    public static int rotateCounterClockwise(int i) {
        Vector2 v = IndexToCoor(i);
        return CoorToIndex(rotateCounterClockwise(v));
    }
}