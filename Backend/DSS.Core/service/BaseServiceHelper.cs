using DSS.Core.entity;
using DSS.Core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DSS.Core.enums.Enum;

namespace DSS.Core.service
{
    public class BaseServiceHelper<TEntity>
    {
        IBaseRepository<TEntity> _baseRepository;

        #region Constructor
        public BaseServiceHelper(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        /// <summary>
        /// Chuẩn hóa dữ liệu 
        /// </summary>
        /// <param name="province"></param>
        /// <param name="w"></param>
        /// <param name="cOption"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> Standardized(List<Province> province, double[] w, double[] cOption, List<ConditionDSS> conditions)
        {
            List<Dictionary<string, object>> listOption = new List<Dictionary<string, object>>();
            for (var i = 0; i < province.Count(); i++)
            {
                Dictionary<string, object> tmp = new Dictionary<string, object>();
                tmp.Add("ProvinceName", province[i].ProvinceName);
                for (var j = 0; j < conditions.Count(); j++)
                {
                    var c = conditions[j].Property;
                    var value = province[i].GetType().GetProperty(c).GetValue(province[i]);
                    if (conditions[j].Status == 0)
                    {
                        if (Convert.ToDouble(value) == 0)
                        {
                            value = 0.001;
                        }

                        cOption[j] += (1 / Convert.ToDouble(value)) * (1 / Convert.ToDouble(value));
                        tmp.Add(conditions[j].Property, 1 / Convert.ToDouble(value));
                    }
                    else
                    {
                        cOption[j] += Convert.ToDouble(value) * Convert.ToDouble(value);
                        tmp.Add(conditions[j].Property, Convert.ToDouble(value));
                    }
                }
                listOption.Add(tmp);
            }
            for (var i = 0; i < cOption.Length; i++)
            {
                cOption[i] = Math.Sqrt(cOption[i]);
            }
            for (var i = 0; i < listOption.Count(); i++)
            {
                for (var j = 0; j < conditions.Count(); j++)
                {
                    var c = conditions[j].Property;
                    listOption[i][c] = (Convert.ToDouble(listOption[i][c]) / cOption[j]) * w[j];
                }
            }
            return listOption;
        }

        /// <summary>
        /// Ra quyết định bằng Topsis
        /// </summary>
        /// <returns></returns>
        public object Topsis(List<ConditionDSS> conditions)
        {

            var w = new double[conditions.Count()];
            var cOption = new double[conditions.Count()];
            for (var i = 0; i < conditions.Count(); i++)
            {
                w[i] = conditions[i].Weight;
                cOption[i] = 0;
            }
            double sum = 0;
            for (var i = 0; i < w.Length; i++)
            {
                sum += w[i];
            }
            if (sum != 1)
            {
                return null;
            }

            List<Province> province = new List<Province>();
            var p = _baseRepository.GetProvince();
            if (p.Code == (int)DSSEnum.ErrorSever)
            {
                return null;
            }
            else
            {
                province = (List<Province>)p.Data;
            }

            var listOption = Standardized(province, w, cOption, conditions);

            var result = OrderOption(listOption);
            return result;
        }
        /// <summary>
        /// Ra quyet dinh cua BS
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="provinceExcept"></param>
        /// <returns></returns>
        public object TopsisDoctor(List<ConditionDSS> conditions, List<Guid> provinceExcept)
        {
            var w = new double[conditions.Count()];
            var cOption = new double[conditions.Count()];
            for (var i = 0; i < conditions.Count(); i++)
            {
                w[i] = conditions[i].Weight;
                cOption[i] = 0;
            }
            double sum = 0;
            for (var i = 0; i < w.Length; i++)
            {
                sum += w[i];
            }
            if (sum != 1)
            {
                return null;
            }

            List<Province> province = new List<Province>();
            var p = _baseRepository.GetProvinceForDoctor(provinceExcept);
            if (p.Code == (int)DSSEnum.ErrorSever)
            {
                return null;
            }
            else
            {
                province = (List<Province>)p.Data;
            }

            var listOption = Standardized(province, w, cOption, conditions);

            var result = OrderOption(listOption);
            return result;
        }
        /// <summary>
        /// Chia đều amount cho k 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="amount"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int[] DevideEqual(int k, int amount, int[] list)
        {
            if (amount == 0)
            {
                return list;
            }
            else
            {
                var tmp = amount / k;
                if (tmp > 0)
                {
                    for (var j = 0; j < k; j++)
                    {
                        list[j] += tmp;
                    }
                    amount %= k;
                }
                else
                {
                    k -= 1;

                }
                return DevideEqual(k, amount, list);
            }

        }

        /// <summary>
        /// Số sau nhỏ hơn số trước. Tổng độ chênh lệch là nhỏ nhất
        /// </summary>
        /// <param name="k"></param>
        /// <param name="amount"></param>
        /// <param name="list"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public int[] DevideOptimal(int k, int amount, int[] list, int step)
        {
            if (amount == 0)
            {
                return list;
            }
            else if (step == 0)
            {
                var sumTmp = 0;
                for (var i = 1; i <= k; i++)
                {
                    sumTmp += i;
                }

                if (sumTmp > amount)
                {
                    k -= 1;
                }
                else
                {
                    for (var i = 0; i < k; i++)
                    {
                        list[i] = k - i;
                    }
                    amount -= sumTmp;
                    step++;
                }

                return DevideOptimal(k, amount, list, step);
            }
            else
            {
                var tmp = amount / k;
                if (tmp > 0)
                {
                    for (var j = 0; j < k; j++)
                    {
                        list[j] += tmp;
                    }
                    amount %= k;
                }
                else
                {
                    k -= 1;

                }
                step++;
                return DevideOptimal(k, amount, list, step);
            }
        }

        /// <summary>
        /// Sắp xếp các ứng cử theo topsis
        /// </summary>
        /// <param name="listOption"></param>
        /// <returns></returns>
        public object OrderOption(List<Dictionary<string, object>> listOption)
        {
            Dictionary<string, object> maxValue = new Dictionary<string, object>();

            Dictionary<string, object>.KeyCollection keys = listOption[0].Keys;
            foreach (string key in keys)
            {
                if (listOption[0][key].GetType() == typeof(string))
                {
                    maxValue.Add(key, "none");
                }
                else
                {
                    maxValue.Add(key, 0.0);
                }

            }

            for (var i = 0; i < listOption.Count(); i++)
            {
                foreach (string key in keys)
                {
                    if (listOption[i][key].GetType() != typeof(string))
                    {
                        if ((double)listOption[i][key] > (double)maxValue[key])
                        {
                            maxValue[key] = listOption[i][key];
                        }
                    }

                }
            }

            return Sort(maxValue, listOption);

        }

        /// <summary>
        /// Sắp xếp kết quả giảm dần về độ ưu tiên
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="listOption"></param>
        /// <returns></returns>
        public object Sort(Dictionary<string, object> maxValue, List<Dictionary<string, object>> listOption)
        {
            Dictionary<string, object>.KeyCollection keys = maxValue.Keys;
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            for (var i = 0; i < listOption.Count(); i++)
            {
                double tmp = 0;
                var pName = "";
                foreach (string key in keys)
                {
                    if (listOption[i][key].GetType() != typeof(string))
                    {
                        tmp += ((double)listOption[i][key] - (double)maxValue[key]) * ((double)listOption[i][key] - (double)maxValue[key]);
                    }
                    else
                    {
                        pName = listOption[i][key].ToString();
                    }
                }
                Dictionary<string, object> tmp2 = new Dictionary<string, object>();
                tmp2.Add("ProvinceName", pName);
                tmp2.Add("Distance", tmp);

                result.Add(tmp2);

            }

            IEnumerable<Dictionary<string, object>> result1 = result as IEnumerable<Dictionary<string, object>>;

            result1 = result1.OrderBy(x => x.ContainsKey("Distance") ? x["Distance"] : 0.0);

            return result1;
        }
        /// <summary>
        /// Electre 
        /// </summary>
        /// <returns></returns>
        public object Electre(List<ConditionDSS> conditions)
        {
            var w = new double[conditions.Count()];
            var cOption = new double[conditions.Count()];
            for (var i = 0; i < conditions.Count(); i++)
            {
                w[i] = conditions[i].Weight;
                cOption[i] = 0;
            }
            double sum = 0;
            for (var i = 0; i < w.Length; i++)
            {
                sum += w[i];
            }
            if (sum != 1)
            {
                return null;
            }

            List<Province> province = new List<Province>();
            var p = _baseRepository.GetProvince();
            if (p.Code == (int)DSSEnum.ErrorSever)
            {
                return null;
            }
            else
            {
                province = (List<Province>)p.Data;
            }

            var listOption = Standardized(province, w, cOption, conditions);

            var result = CacluateIndexElectre(listOption, w);
            return result;
        }
        /// <summary>
        /// Tinh chi so phu hop, khong phu hop
        /// </summary>
        /// <param name="listOption"></param>
        /// <param name="w"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public object CacluateIndexElectre(List<Dictionary<string, object>> listOption, double[] w)
        {
            var C = new List<int>[listOption.Count(), listOption.Count()];
            var D = new List<int>[listOption.Count(), listOption.Count()];
            var C1 = new double[listOption.Count(), listOption.Count()];
            var D1 = new double[listOption.Count(), listOption.Count()];
            var sum = new double[listOption.Count(), listOption.Count()];
            Dictionary<string, object>.KeyCollection keys = listOption[0].Keys;
            //Tinh tap phu hop, khong phu hop
            for (var i = 0; i < listOption.Count(); i++)
            {
                for(var j=0; j<listOption.Count(); j++)
                {
                    if(j != i)
                    {
                        List<int> optionC = new List<int>();
                        List<int> optionD = new List<int>();
                        int tmp = 0;
                        double sumT = 0;
                        foreach (string key in keys)
                        {
                            if (listOption[i][key].GetType() != typeof(string))
                            {
                                if ((double)listOption[i][key] >= (double)listOption[j][key])
                                {
                                    optionC.Add(tmp);
                                }
                                else
                                {
                                    optionD.Add(tmp);
                                }
                                sumT += Math.Abs((double)listOption[i][key] - (double)listOption[j][key]);
                                tmp++;
                            }
                        }
                        C[i, j] = optionC;
                        D[i, j] = optionD;
                        sum[i, j] = sumT;
                    }
                    
                }
                
            }
            //Tinh chi so phu hop, khong phu hop
            for(var i=0; i<listOption.Count(); i++)
            {
                for(var j=0; j<listOption.Count(); j++)
                {
                    if(j != i)
                    {
                        //Tinh chi so phu hop cua i va j
                        for(var l=0; l<w.Length; l++)
                        {
                            if(C[i, j].Contains(l))
                            {
                                C1[i, j] += w[l];
                            }
                        }
                        //Tinh chi so khong phu hop cua i va j
                        int tmp = 0;
                        double sumT = 0;
                        foreach (string key in keys)
                        {
                            if (listOption[i][key].GetType() != typeof(string))
                            {
                                if(D[i, j].Contains(tmp))
                                {
                                    sumT += Math.Abs((double)listOption[i][key] - (double)listOption[j][key]);
                                }
                                tmp++;
                            }
                        }
                        if(sum[i, j] == 0)
                        {
                            D1[i, j] = 1;
                        }
                        else
                        {
                            D1[i, j] = (double)sumT / sum[i, j];
                        }
                        
                    }
                }
            }

            return SelectObjectByElec(listOption, C1, D1);
           
        }

        /// <summary>
        /// Lua chon doi tuong thoa man
        /// </summary>
        /// <param name="listOption"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public object SelectObjectByElec(List<Dictionary<string, object>> listOption, double[,] C1, double[,] D1)
        {
            double C = 0;
            double D = 0;
            double sumT1 = 0;
            double sumT2 = 0;
            for (var i=0; i<listOption.Count(); i++)
            {
                for(var j=0; j<listOption.Count(); j++)
                {
                    if(i != j)
                    {
                        sumT1 += C1[i, j];
                        sumT2 += D1[i, j];
                    }
                }
            }
            //63 tinh
            C = sumT1 / 3906;
            D = sumT2 / 3906;

            var DST = new List<int>[listOption.Count()];
            for(var i=0; i<DST.Length; i++)
            {
                var tmp = new List<int>();
                DST[i] = tmp;
            }

            for (var i = 0; i < listOption.Count(); i++)
            {
                for (var j = 0; j < listOption.Count(); j++)
                {
                    if (i != j)
                    {
                        if (C1[i, j] >= C && D1[i, j] < D)
                        {
                            var x = DST[j];
                            x.Add(i);
                            DST[j] = x;
                        }
                    }
                }
            }
            List<object> result = new List<object>();
            for(var i=0; i<DST.Count(); i++)
            {
                if(DST[i].Count() == 0)
                {
                    result.Add(listOption[i]);
                }
            }
            
            return result;
        }
        
    }
}
