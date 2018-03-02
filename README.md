# iFlyDotNet
.Net调用讯飞ASR和TTS
## ASR
```csharp
private FlyIsr _isr;
//appid
private const string C1 = "appid=57aace1b";
//解码参数
private const string C2 = "sub=iat,ptt = 0, rate=16000,ent=sms16k,rst=plain,vad_eos=3000,plain = utf-8 ,aue = speex-wb";
private void button1_MouseDown(object sender, MouseEventArgs e)
{
    _isr = new FlyIsr(C1, C2);
    _isr.DataArrived += (s, ee) => MessageBox.Show(@"识别结果：" + ee.Result);
    _isr.StartRecoding();

}

private void button1_MouseUp(object sender, MouseEventArgs e)
{
    _isr.StopRecoding();
}
```
## TTS
暂未封装完成
