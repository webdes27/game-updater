﻿using System.Diagnostics;
using System.IO;
using com.jds.GUpdater.classes.language;
using com.jds.GUpdater.classes.listloader.enums;

namespace com.jds.GUpdater.classes.games.propertyes.impl
{
    /**
     * Author: VISTALL
     * На заметку PasswordPropertyTextAttribute
     */

    public class AionProperty : GameProperty
    {
        public override Game GameEnum()
        {
            return Game.AION;
        }

        public override ProcessStartInfo GetStartInfo()
        {
            if (!Installed || !isEnable() || !_loader.IsValid || !this[ListFileType.CRITICAL])
            {
                return null;
            }

            string path = Path + "\\bin32\\aion.exe";

            if (!File.Exists(path))
                return null;

            var info = new ProcessStartInfo(path)
                           {
                               Arguments = "сс:2 -ip:213.186.118.75 -port:2109 -ng -noweb"
                           };

            return info;
        }

        public override string listURL()
        {
            var lang = "en";
            if(LanguageHolder.Instance().Language != null)
            {
                lang = LanguageHolder.Instance().Language.ShortName;
            }
            return "http://ru.aionwars.com/aion/" + lang + "/";
        }

        public override string getKey()
        {
            return "Software\\J Develop Station\\GUpdater\\Aion";
        }
    }
}