using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOManager.Helpers.Exceptions
{
    public class CanNotFindItemIndexException :Exception
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanNotFindItemIndexException()
            : base("do nothing")
        {
            // 普通のコンストラクタ
        }
        /// <summary>
        /// メッセージ文字列を渡すコンストラクタ
        /// </summary>
        /// <param name="message">メッセージ文字列</param>
        public CanNotFindItemIndexException(string message)
            : base(message)
        {
            // メッセージ文字列を渡すコンストラクタ
        }
        /// <summary>
        /// メッセージ文字列と発生済み例外オブジェクトを渡すコンストラクタ
        /// </summary>
        /// <param name="message">メッセージ文字列</param>
        /// <param name="innerException">発生済み例外オブジェクト</param>
        public CanNotFindItemIndexException(string message, Exception innerException)
            : base(message, innerException)
        {
            // メッセージ文字列と発生済み例外オブジェクトを渡すコンストラクタ
        }

    }
}
