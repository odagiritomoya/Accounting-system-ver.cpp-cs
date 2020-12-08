#pragma once
#include <string>
#include <atlstr.h>

//ここでのみ使うシンボル定義　現在の文字コードに対応するクラスとその反対のクラス
//現在の文字コードの反対の文字型のスイッチ
#ifdef _UNICODE
	#define MY_STRING_0028 wstring
	#define MY_OTHER_STRING_0028 string
	#define MY_OTHER_CHAR char
#else
	#define MY_STRING_0028 string
	#define MY_OTHER_STRING_0028 wstring
	#define MY_OTHER_CHAR wchar_t
#endif

namespace MyTools {

	//文字列変換
	std::string WStringToString(std::wstring istr);
	std::wstring StringToWString(std::string istr);

	std::string UTF8toSJIS(const std::string strUTF8);

	//フォーマット
	template <typename ... Args>
	std::MY_STRING_0028 format(const std::MY_STRING_0028 fmt, Args ... args)
	{
		CString rtn;
		rtn.Format(fmt.c_str(), args...);
		return rtn.GetBuffer();
	}

	class TString : public std::MY_STRING_0028 {
		//コンストラクタの継承（親クラスのやつを引き継ぐ）
		using std::MY_STRING_0028::MY_STRING_0028;
	public:
		//追加するコンストラクタ
		TString(void);
		TString(std::MY_OTHER_STRING_0028 str);
		TString(MY_OTHER_CHAR ch);
		TString(MY_OTHER_CHAR* str);
		TString(std::MY_STRING_0028 str);
		//作成するキャスト変換
		operator std::MY_OTHER_STRING_0028() const;
		operator CString() const;
		//親クラスも含めた演算子=のオーバーロードたち
		TString operator=(std::MY_STRING_0028 str);
		TString operator=(std::MY_OTHER_STRING_0028 str);
		TString operator=(CString str);
		TString operator=(MY_OTHER_CHAR ch);
		TString operator=(MY_OTHER_CHAR* str);
		//テンプレート関数
		template <typename ... Args>
		std::MY_STRING_0028 format(const std::MY_STRING_0028 fmt, Args ... args);
	};
}