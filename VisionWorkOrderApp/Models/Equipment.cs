using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get;  set; }
        public string Name { get;  set; }

        //빈생성자 추가
        public Equipment() { }
        public Equipment(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    //빈 생성자가 추가되는이유 
    /*
     * DB 에서 데이터 읽을 때
     → 빈 객체 먼저 생성 (기본 생성자 호출)
     → 그 다음 각 속성에 값 채워줌
     기본 생성자 없으면
     → 객체 생성 자체가 안 됨 → 앱 실행 안 됨!
     */
}
