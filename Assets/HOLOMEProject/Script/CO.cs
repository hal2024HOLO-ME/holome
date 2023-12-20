using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const
{
    /// <summary>
    /// 定数を管理するクラス
    /// </summary>
    public class CO : MonoBehaviour
    {
        /// <summary>
        /// 懐き度の最大値
        /// </summary>
        public const int MAX_NOSTALGIC_LEVEL = 100;
        /// <summary>
        /// 懐き度の最小値
        /// </summary>
        public const int MIN_NOSTALGIC_LEVEL = 0;

        /// <summary>
        /// アニメーションのトリガー名：歩く
        /// </summary>
        public const string ANIMATOR_TRIGGER_WALK = "WalkTrigger";

        /// <summary>
        /// アニメーションのbool名：死ぬ
        /// </summary>
        public const string ANIMATOR_BOOL_DEAD = "isDead";

        /// <summary>
        /// アニメーションのbool名：寝る
        /// </summary>
        public const string ANIMATOR_BOOL_SLEEP = "isSleep";

    }
}
