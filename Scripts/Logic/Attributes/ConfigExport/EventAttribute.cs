using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// EventAttribute
/// </summary>
public partial class EventAttribute
{
    /// <summary>
    /// 事件模板
    /// </summary>
    public Event_template event_template;
    /// <summary>
    /// 路点模板
    /// </summary>
    public WP_template wp_template;
    /// <summary>
    /// 队伍属性
    /// </summary>
    public TeamAttribute teamAttribute;
    /// <summary>
    /// 选项属性
    /// </summary>
    public SelectionAttribute selectionAttribute;
}
