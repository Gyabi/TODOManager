using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TODOManager.Presentation.ViewModels.Contents
{
    /// <summary>
    /// Detailの内容を解析して親子関係を算出する
    /// TODO:正規表現を使ってスマートに処理したい
    /// </summary>
    public class DetailParser
    {
        /// <summary>
        /// 解析に使う文字列
        /// </summary>
        public string pattern = ">";

        public string detail;

        public DetailParser(string detail)
        {
            this.detail = detail;
        }

        /// <summary>
        /// 親子関係をもった解析済みインスタンスを返却する
        /// </summary>
        /// <returns></returns>
        public ParseRootData Execute()
        {
            //構文解析用親クラスを生成
            ParseRootData root = new ParseRootData(detail);
            //解析開始
            root.Parse(pattern);

            return root;
        }
    }

    /// <summary>
    /// 解析した構造対応するクラス
    /// </summary>
    public class ParseRootData
    {
        /// <summary>
        /// 解析する元データ
        /// </summary>
        public string rawData { get; set; }
        /// <summary>
        /// 行ごとに分割して情報を付与したList
        /// </summary>
        public List<ParseData> parseDatas = new List<ParseData>();
        /// <summary>
        /// 親オブジェクトのdetail
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 子要素
        /// </summary>
        public List<ParseChildData> childs { get; set; } = new List<ParseChildData>();
        
        public ParseRootData(string rawData)
        {
            this.rawData = rawData;
        }

        public void Parse(string pattern)
        {
            //分割して指定の文字列がネストしている行とそのネスト数を算出してlistに注入
            foreach(var (value, index) in rawData.Split("\n").Select((value, index) => (value, index)))
            {
                //選択した文字列から開始する行が判定＋trueの場合はその文字数を格納
                int count = 0;
                if(Regex.IsMatch(value, "^"+pattern+"+"))
                {
                    //TODO：行に存在するすべての文字列から選択した文字列を検索（重複を含めて）
                    //してその最長文字数をcountとして代入している。よって行の先頭以外にも含まれる
                    //場合にバグとなる可能性がある
                    MatchCollection matches = Regex.Matches(value, "^"+pattern + "+");
                    count = matches.Select(x => x.Value).Max().Length;
                }

                //パターンの文字列は削除しておく
                this.parseDatas.Add(new ParseData(value, count, index));
            }

            //countから0をのぞく最小値を算出
            //対象が存在しないならとりあえず100を入れておく
            int minCount = 100;
            if(this.parseDatas.Any(x => x.count > 0))
            {
                minCount = this.parseDatas.Select(x => x.count).Where(x => x > 0).Min();
            }

            //rootのdetailを特定
            bool flag = true;
            ParseData[] tmpParseDatas = this.parseDatas.ToArray();
            List<string> detailDatas = new List<string>();
            foreach(ParseData parseData in tmpParseDatas)
            {
                if(parseData.count == 0 && flag)
                {
                    //改行を付けて追加
                    detailDatas.Add(parseData.data);
                    //リストからは削除
                    this.parseDatas.Remove(parseData);
                }
                else
                {
                    flag = false;
                }
            }
            if (detailDatas.Count != 0) this.detail = string.Join("\n", detailDatas);

            //子要素を生成する
            ParseData titleData = null;
            List<ParseData> childParseDatas = new List<ParseData>();
            foreach(ParseData parseData in this.parseDatas)
            {
                //タイトルとなる変数が空で最小ネスト数の要素なら
                if(titleData == null && parseData.count == minCount)
                {
                    titleData = parseData;
                }
                else if(titleData != null && parseData.count == minCount)
                {
                    //次のmincountの要素が出てきたらここまでの内容を登録
                    this.childs.Add(new ParseChildData(titleData, childParseDatas));
                    //次のループに向けて初期化
                    titleData = parseData;
                    childParseDatas = new List<ParseData>();
                }
                else if(titleData != null && parseData.count != minCount)
                {
                    //detailに含まれる内容が入っているのでリストに追加
                    childParseDatas.Add(parseData);
                }
            }

            //ループが最後まで回ったときに最小ネストを検知していれば最後の子要素を生成する
            if(titleData != null)
            {
                this.childs.Add(new ParseChildData(titleData, childParseDatas));
            }

            //子要素すべてのParse処理を実行
            foreach(ParseChildData data in this.childs)
            {
                data.Parse(pattern);
            }
        }
    }

    public class ParseChildData
    {
        public ParseData titleData{ get; set; }
        public List<ParseData> parseDatas{ get; set; }
        public string detail { get; set; }
        /// <summary>
        /// タスクがアクティブか判定（文末の☑を見る）
        /// </summary>
        public bool isActive { get; set; }
        public List<ParseChildData> childs { get; set; } = new List<ParseChildData>();

        /// <summary>
        /// タスクのアクティブを判定する為の末尾に設けるフラグ文字
        /// </summary>
        public static char activeFlag = '☑';
    
        public ParseChildData(ParseData titleData, List<ParseData> parseDatas)
        {
            this.titleData = titleData;
            this.parseDatas = parseDatas;

            //アクティブ状態を判定して処理
            if (this.titleData.data[this.titleData.data.Length-1] == activeFlag)
            {
                this.isActive = true;
                //最後の判定文字は削除
                this.titleData.data.Remove(this.titleData.data.Length - 1);
            }
            else
            {
                this.isActive = false;
            }
        }

        public void Parse(string pattern)
        {
            //countから0をのぞく最小値を算出
            //対象が存在しないならとりあえず100を入れておく
            int minCount = 100;
            if (this.parseDatas.Any(x => x.count > 0))
            {
                minCount = this.parseDatas.Select(x => x.count).Where(x => x > 0).Min();
            }
            //detailを特定
            bool flag = true;
            ParseData[] tmpParseDatas = this.parseDatas.ToArray();
            List<string> detailDatas = new List<string>();
            foreach (ParseData parseData in tmpParseDatas)
            {
                if (parseData.count == 0 && flag)
                {
                    //改行を付けて追加
                    detailDatas.Add(parseData.data);
                    //リストからは削除
                    this.parseDatas.Remove(parseData);
                }
                else
                {
                    flag = false;
                }
            }
            if (detailDatas.Count != 0) this.detail = string.Join("\n", detailDatas);

            //子要素を生成する
            ParseData titleData = null;
            List<ParseData> childParseDatas = new List<ParseData>();
            foreach (ParseData parseData in this.parseDatas)
            {
                //タイトルとなる変数が空で最小ネスト数の要素なら
                if (titleData == null && parseData.count == minCount)
                {
                    titleData = parseData;
                }
                else if (titleData != null && parseData.count == minCount)
                {
                    //次のmincountの要素が出てきたらここまでの内容を登録
                    this.childs.Add(new ParseChildData(titleData, childParseDatas));

                    //次のループに向けて初期化
                    titleData = parseData;
                    childParseDatas = new List<ParseData>();
                }
                else if (titleData != null && parseData.count != minCount)
                {
                    //detailに含まれる内容が入っているのでリストに追加
                    childParseDatas.Add(parseData);
                }
            }

            //ループが最後まで回ったときに最小ネストを検知していれば最後の子要素を生成する
            if (titleData != null)
            {
                this.childs.Add(new ParseChildData(titleData, childParseDatas));
            }

            //子要素すべてのParse処理を実行
            foreach (ParseChildData data in this.childs)
            {
                data.Parse(pattern);
            }

        }
    }

    public class ParseData
    {
        public string data { get; set; }
        public int count { get; set; }
        public int index { get; set; }

        public ParseData(string data, int count, int index)
        {
            this.data = data;
            this.count = count;
            this.index = index;
        }
    }
}
