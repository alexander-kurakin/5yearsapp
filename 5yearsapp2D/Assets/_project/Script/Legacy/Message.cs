using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 public class Message {

        private String key;
        private String value;

        private Message() { }

        public Message(String key, String value)
        {
            this.key = key;
            this.value = value;
        }

        public String getKey()
        {
            return key;
        }

        public String getValue()
        {
            return value;
        }

}
