using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreForFrame : MonoBehaviour
{
    [SerializeField]
    private int _frameNumber;
    public int FrameNumber
    { get { return _frameNumber; } private set { } }

    [SerializeField]
    private TMP_Text _Cast1;
    [SerializeField] 
    private TMP_Text _Cast2;
    [SerializeField]
    private TMP_Text _total;

    private int _scoreCast1;
    private int _scoreCast2;
    private int _totalScore;

    private FrameStatus _status;
    public FrameStatus Status
    { 
        get { return _status; }
        set { switch (_status)
            { case FrameStatus.Strike: _status = FrameStatus.Spea; break; 
              case FrameStatus.Spea: _status = FrameStatus.None;  break;}
        }
    }

    private void Awake()
    {
        _status = FrameStatus.None;
    }
    public void Write_cast1( int score)
    {
        _scoreCast1 = score;
        switch ( score )
        {
            case 0: { _Cast1.text = "_"; }
                break;
            case 10: { _Cast2.text = "X"; _status = FrameStatus.Strike; }
                break;
            default: { _Cast1.text = _scoreCast1.ToString(); } 
                break;
        }
    }
    public void Write_cast2(int score)
    {   
        _scoreCast2 = score;
        switch (_scoreCast2)
        {
            case 0:
                { _Cast2.text = "_"; _status = FrameStatus.None; }
                break;
            default:
                {
                    if (_scoreCast2 + _scoreCast1 == 10)
                    { _Cast2.text = @"\"; _status = FrameStatus.Spea; }
                    else
                    {
                        _Cast2.text = _scoreCast2.ToString();
                        _status = FrameStatus.None;
                    }
                    break;
                }

        }
    }
    public int WriteTotal( int score)
    {
        _totalScore = _scoreCast1 + _scoreCast2 + score;
        _total.text = _totalScore.ToString();
        return _totalScore;
    }
    public int AddAtTotal(int score)
    { 
        _totalScore += score;
        _total.text = _totalScore.ToString();
        return _totalScore;
    }
}
