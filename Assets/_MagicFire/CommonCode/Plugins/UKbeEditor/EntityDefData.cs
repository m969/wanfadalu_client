using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDefData : ScriptableObject
{
    [SerializeField]
    private List<string> _interfaceList;

    [SerializeField]
    private List<DefProperty> _propertyList;

    [SerializeField]
    private List<DefMethod> _baseMethodList;

    [SerializeField]
    private List<DefMethod> _cellMethodList;

    [SerializeField]
    private List<DefMethod> _clientMethodList;


    [Serializable]
    private struct DefProperty
    {
        [SerializeField]
        private string _propertyName;

        [SerializeField]
        private DefType _type;

        [SerializeField]
        private DefFlag _flags;

        [SerializeField]
        private bool _persistent;

        [SerializeField]
        private string _default;
    }

    [Serializable]
    private enum DefType
    {
        UINT8,
        UINT16,
        UINT32,
        UINT64,
        INT8,
        INT16,
        INT32,
        INT64,
        FLOAT,
        DOUBLE,
        VECTOR2,
        VECTOR3,
        VECTOR4,
        STRING,
        UNICODE,
        PYTHON,
        PY_DICT,
        PY_TUPLE,
        PY_LIST,
        MAILBOX,
        BLOB
    }

    [Serializable]
    private enum DefFlag
    {
        BASE,
        BASE_AND_CLIENT,
        CELL_PRIVATE,
        CELL_PUBLIC,
        CELL_PUBLIC_AND_OWN,
        ALL_CLIENTS,
        OWN_CLIENT,
        OTHER_CLIENTS
    }

    [Serializable]
    private struct DefMethod
    {
        [SerializeField]
        private string _methodName;
        [SerializeField]
        private bool _exposed;
        [SerializeField]
        private List<DefType> _args;
    }
}
