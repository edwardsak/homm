using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Heroes.Core
{
    public class AnimationData
    {
        public int _id;
        public string _purpose;
        public string _folder;
        public string _prefix;
        public string _ext;
        public int _moveSpeed;
        public int _turnPerFrame;
        public string[] _fileNos;

        public AnimationData()
        {
            _id = 0;
            _purpose = "";
            _folder = "";
            _prefix = "";
            _ext = "";
            _moveSpeed = 0;
            _turnPerFrame = 0;
            _fileNos = null;
        }

    }
}
