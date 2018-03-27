/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(a){(function(){function F(){for(var b=a.get_visible_events(),d=[],c=0;c<this.y_unit.length;c++)d[c]=[];d[f]||(d[f]=[]);for(c=0;c<b.length;c++){for(var f=this.order[b[c][this.y_property]],g=0;this._trace_x[g+1]&&b[c].start_date>=this._trace_x[g+1];)g++;for(;this._trace_x[g]&&b[c].end_date>this._trace_x[g];)d[f][g]||(d[f][g]=[]),d[f][g].push(b[c]),g++}return d}function u(b,d,c){var f=0,g=d?b.end_date:b.start_date;if(g.valueOf()>a._max_date.valueOf())g=a._max_date;var h=g-a._min_date_timeline;
if(h<0)l=0;else{var j=Math.round(h/(c*a._cols[0]));if(j>a._cols.length)j=a._cols.length;for(var e=0;e<j;e++)f+=a._cols[e];var k=a.date.add(a._min_date_timeline,a.matrix[a._mode].x_step*j,a.matrix[a._mode].x_unit),h=g-k,l=Math.floor(h/c)}f+=d?l-14:l+1;return f}function G(b){var d="";if(b&&this.render!="cell"){b.sort(function(a,b){return a.start_date.valueOf()==b.start_date.valueOf()?a.id>b.id?1:-1:a.start_date>b.start_date?1:-1});for(var c=[],f=b.length,g=0;g<f;g++){var h=b[g];for(h._inner=!1;c.length;)if(c[c.length-
1].end_date.valueOf()<=h.start_date.valueOf())c.splice(c.length-1,1);else break;for(var j=!1,e=0;e<c.length;e++){var k=c[e];if(k.end_date.valueOf()<=h.start_date.valueOf()){j=!0;h._sorder=k._sorder;c.splice(e,1);h._inner=!0;break}}if(c.length)c[c.length-1]._inner=!0;if(!j)if(c.length)if(c.length<=c[c.length-1]._sorder){if(c[c.length-1]._sorder)for(var l=0;l<c.length;l++){for(var i=!1,m=0;m<c.length;m++)if(c[m]._sorder==l){i=!0;break}if(!i){h._sorder=l;break}}else h._sorder=0;h._inner=!0}else{for(var i=
c[0]._sorder,n=1;n<c.length;n++)if(c[n]._sorder>i)i=c[n]._sorder;h._sorder=i+1;h._inner=!1}else h._sorder=0;c.push(h);c.length>(c.max_count||0)?(c.max_count=c.length,h._count=c.length):h._count=h._count?h._count:1}for(var o=0;o<b.length;o++)b[o]._count=c.max_count;for(var p=0;p<f;p++)d+=a.render_timeline_event.call(this,b[p],!1)}return d}function H(b){var d="<table style='table-layout:fixed;' cellspacing='0' cellpadding='0'>",c=[];a._load_mode&&a._load();if(this.render=="cell")c=F.call(this);else for(var f=
a.get_visible_events(),g=0;g<f.length;g++){var h=this.order[f[g][this.y_property]];c[h]||(c[h]=[]);c[h].push(f[g])}for(var j=0,e=0;e<a._cols.length;e++)j+=a._cols[e];var k=new Date;this._step=k=(a.date.add(k,this.x_step*this.x_size,this.x_unit)-k)/j;this._summ=j;var l=a._colsS.heights=[];this._events_height={};this._section_height={};for(e=0;e<this.y_unit.length;e++){var i=this._logic(this.render,this.y_unit[e],this);a._merge(i,{height:this.dy});if(this.section_autoheight){if(this.y_unit.length*i.height<
b.offsetHeight)i.height=Math.max(i.height,Math.floor((b.offsetHeight-1)/this.y_unit.length));this._section_height[this.y_unit[e].key]=i.height}a._merge(i,{tr_className:"",style_height:"height:"+i.height+"px;",style_width:"width:"+(this.dx-1)+"px;",td_className:"dhx_matrix_scell"+(a.templates[this.name+"_scaley_class"](this.y_unit[e].key,this.y_unit[e].label,this.y_unit[e])?" "+a.templates[this.name+"_scaley_class"](this.y_unit[e].key,this.y_unit[e].label,this.y_unit[e]):""),td_content:a.templates[this.name+
"_scale_label"](this.y_unit[e].key,this.y_unit[e].label,this.y_unit[e]),summ_width:"width:"+j+"px;",table_className:""});var m=G.call(this,c[e]);if(this.fit_events){var n=this._events_height[this.y_unit[e].key]||0;i.height=n>i.height?n:i.height;i.style_height="height:"+i.height+"px;";this._section_height[this.y_unit[e].key]=i.height}d+="<tr class='"+i.tr_className+"' style='"+i.style_height+"'><td class='"+i.td_className+"' style='"+i.style_width+" height:"+(i.height-1)+"px;'>"+i.td_content+"</td>";
if(this.render=="cell")for(g=0;g<a._cols.length;g++)d+="<td class='dhx_matrix_cell "+a.templates[this.name+"_cell_class"](c[e][g],this._trace_x[g],this.y_unit[e])+"' style='width:"+(a._cols[g]-1)+"px'><div style='width:"+(a._cols[g]-1)+"px'>"+a.templates[this.name+"_cell_value"](c[e][g])+"</div></td>";else{d+="<td><div style='"+i.summ_width+" "+i.style_height+" position:relative;' class='dhx_matrix_line'>";d+=m;d+="<table class='"+i.table_className+"' cellpadding='0' cellspacing='0' style='"+i.summ_width+
" "+i.style_height+"' >";for(g=0;g<a._cols.length;g++)d+="<td class='dhx_matrix_cell "+a.templates[this.name+"_cell_class"](c[e],this._trace_x[g],this.y_unit[e])+"' style='width:"+(a._cols[g]-1)+"px'><div style='width:"+(a._cols[g]-1)+"px'></div></td>";d+="</table>";d+="</div></td>"}d+="</tr>"}d+="</table>";this._matrix=c;b.innerHTML=d;a._rendered=[];for(var o=a._obj.getElementsByTagName("DIV"),e=0;e<o.length;e++)o[e].getAttribute("event_id")&&a._rendered.push(o[e]);this._scales={};for(e=0;e<b.firstChild.rows.length;e++){l.push(b.firstChild.rows[e].offsetHeight);
var p=this.y_unit[e].key,q=this._scales[p]=a._isRender("cell")?b.firstChild.rows[e]:b.firstChild.rows[e].childNodes[1].getElementsByTagName("div")[0];a.callEvent("onScaleAdd",[q,p])}}function I(b){var d=a.xy.scale_height,c=this._header_resized||a.xy.scale_height;a._cols=[];a._colsS={height:0};this._trace_x=[];var f=a._x-this.dx-18,g=[this.dx],h=a._els.dhx_cal_header[0];h.style.width=g[0]+f+"px";for(var j=a._min_date_timeline=a._min_date,e=0;e<this.x_size;e++)this._trace_x[e]=new Date(j),j=a.date.add(j,
this.x_step,this.x_unit),a._cols[e]=Math.floor(f/(this.x_size-e)),f-=a._cols[e],g[e+1]=g[e]+a._cols[e];b.innerHTML="<div></div>";if(this.second_scale){for(var k=this.second_scale.x_unit,l=[this._trace_x[0]],i=[],m=[this.dx,this.dx],n=0,o=0;o<this._trace_x.length;o++){var p=this._trace_x[o],q=A(k,p,l[n]);q&&(++n,l[n]=p,m[n+1]=m[n]);var s=n+1;i[n]=a._cols[o]+(i[n]||0);m[s]+=a._cols[o]}b.innerHTML="<div></div><div></div>";var v=b.firstChild;v.style.height=c+"px";var t=b.lastChild;t.style.position="relative";
for(var w=0;w<l.length;w++){var u=l[w],y=a.templates[this.name+"_second_scalex_class"](u),x=document.createElement("DIV");x.className="dhx_scale_bar dhx_second_scale_bar"+(y?" "+y:"");a.set_xy(x,i[w]-1,c-3,m[w],0);x.innerHTML=a.templates[this.name+"_second_scale_date"](u);v.appendChild(x)}}a.xy.scale_height=c;for(var b=b.lastChild,r=0;r<this._trace_x.length;r++){j=this._trace_x[r];a._render_x_header(r,g[r],j,b);var z=a.templates[this.name+"_scalex_class"](j);z&&(b.lastChild.className+=" "+z)}a.xy.scale_height=
d;var B=this._trace_x;b.onclick=function(b){var c=C(b);c&&a.callEvent("onXScaleClick",[c.x,B[c.x],b||event])};b.ondblclick=function(b){var c=C(b);c&&a.callEvent("onXScaleDblClick",[c.x,B[c.x],b||event])}}function A(b,d,c){switch(b){case "hour":return d.getHours()!=c.getHours()||A("day",d,c);case "day":return!(d.getDate()==c.getDate()&&d.getMonth()==c.getMonth()&&d.getFullYear()==c.getFullYear());case "week":return!(a.date.getISOWeek(d)==a.date.getISOWeek(c)&&d.getFullYear()==c.getFullYear());case "month":return!(d.getMonth()==
c.getMonth()&&d.getFullYear()==c.getFullYear());case "year":return d.getFullYear()!=c.getFullYear();default:return!1}}function J(b){if(b){a.set_sizes();D();var d=a._min_date;I.call(this,a._els.dhx_cal_header[0]);H.call(this,a._els.dhx_cal_data[0]);a._min_date=d;a._els.dhx_cal_date[0].innerHTML=a.templates[this.name+"_date"](a._min_date,a._max_date)}}function E(){if(a._tooltip)a._tooltip.style.display="none",a._tooltip.date=""}function K(b,d,c){if(b.render=="cell"){var f=d.x+"_"+d.y,g=b._matrix[d.y][d.x];
if(!g)return E();g.sort(function(a,b){return a.start_date>b.start_date?1:-1});if(a._tooltip){if(a._tooltip.date==f)return;a._tooltip.innerHTML=""}else{var h=a._tooltip=document.createElement("DIV");h.className="dhx_tooltip";document.body.appendChild(h);h.onclick=a._click.dhx_cal_data}for(var j="",e=0;e<g.length;e++){var k=g[e].color?"background-color:"+g[e].color+";":"",l=g[e].textColor?"color:"+g[e].textColor+";":"";j+="<div class='dhx_tooltip_line' event_id='"+g[e].id+"' style='"+k+""+l+"'>";j+=
"<div class='dhx_tooltip_date'>"+(g[e]._timed?a.templates.event_date(g[e].start_date):"")+"</div>";j+="<div class='dhx_event_icon icon_details'>&nbsp;</div>";j+=a.templates[b.name+"_tooltip"](g[e].start_date,g[e].end_date,g[e])+"</div>"}a._tooltip.style.display="";a._tooltip.style.top="0px";a._tooltip.style.left=document.body.offsetWidth-c.left-a._tooltip.offsetWidth<0?c.left-a._tooltip.offsetWidth+"px":c.left+d.src.offsetWidth+"px";a._tooltip.date=f;a._tooltip.innerHTML=j;a._tooltip.style.top=document.body.offsetHeight-
c.top-a._tooltip.offsetHeight<0?c.top-a._tooltip.offsetHeight+d.src.offsetHeight+"px":c.top+"px"}}function D(){dhtmlxEvent(a._els.dhx_cal_data[0],"mouseover",function(b){var d=a.matrix[a._mode];if(d&&d.render=="cell"){if(d){var c=a._locate_cell_timeline(b),b=b||event,f=b.target||b.srcElement;if(c)return K(d,c,getOffset(c.src))}E()}});D=function(){}}function L(a){for(var d=a.parentNode.childNodes,c=0;c<d.length;c++)if(d[c]==a)return c;return-1}function C(a){for(var a=a||event,d=a.target?a.target:a.srcElement;d&&
d.tagName!="DIV";)d=d.parentNode;if(d&&d.tagName=="DIV"){var c=d.className.split(" ")[0];if(c=="dhx_scale_bar")return{x:L(d),y:-1,src:d,scale:!0}}}a.matrix={};a._merge=function(a,d){for(var c in d)typeof a[c]=="undefined"&&(a[c]=d[c])};a.createTimelineView=function(b){a._merge(b,{section_autoheight:!0,name:"matrix",x:"time",y:"time",x_step:1,x_unit:"hour",y_unit:"day",y_step:1,x_start:0,x_size:24,y_start:0,y_size:7,render:"cell",dx:200,dy:50,event_dy:a.xy.bar_height-5,event_min_dy:a.xy.bar_height-
5,resize_events:!0,fit_events:!0,second_scale:!1,_logic:function(b,c,d){var e={};a.checkEvent("onBeforeViewRender")&&(e=a.callEvent("onBeforeViewRender",[b,c,d]));return e}});a.checkEvent("onTimelineCreated")&&a.callEvent("onTimelineCreated",[b]);var d=a.render_data;a.render_data=function(c,h){if(this._mode==b.name)if(h)for(var j=0;j<c.length;j++)this.clear_event(c[j]),this.render_timeline_event.call(this.matrix[this._mode],c[j],!0);else a.renderMatrix.call(b,!0,!0);else return d.apply(this,arguments)};
a.matrix[b.name]=b;a.templates[b.name+"_cell_value"]=function(a){return a?a.length:""};a.templates[b.name+"_cell_class"]=function(){return""};a.templates[b.name+"_scalex_class"]=function(){return""};a.templates[b.name+"_second_scalex_class"]=function(){return""};a.templates[b.name+"_scaley_class"]=function(){return""};a.templates[b.name+"_scale_label"]=function(a,b){return b};a.templates[b.name+"_tooltip"]=function(a,b,c){return c.text};a.templates[b.name+"_date"]=function(b,c){return b.getDay()==
c.getDay()&&c-b<864E5?a.templates.day_date(b):a.templates.week_date(b,c)};a.templates[b.name+"_scale_date"]=a.date.date_to_str(b.x_date||a.config.hour_date);a.templates[b.name+"_second_scale_date"]=a.date.date_to_str(b.second_scale&&b.second_scale.x_date?b.second_scale.x_date:a.config.hour_date);a.date["add_"+b.name]=function(c,d){return a.date.add(c,(b.x_length||b.x_size)*d*b.x_step,b.x_unit)};a.date[b.name+"_start"]=function(c){var d=a.date[b.x_unit+"_start"]||a.date.day_start,f=d.call(a.date,c);
return f=a.date.add(f,b.x_step*b.x_start,b.x_unit)};a.attachEvent("onSchedulerResize",function(){return this._mode==b.name?(a.renderMatrix.call(b,!0,!0),!1):!0});a.attachEvent("onOptionsLoad",function(){b.order={};a.callEvent("onOptionsLoadStart",[]);for(var c=0;c<b.y_unit.length;c++)b.order[b.y_unit[c].key]=c;a.callEvent("onOptionsLoadFinal",[]);a._date&&b.name==a._mode&&a.setCurrentView(a._date,a._mode)});a.callEvent("onOptionsLoad",[b]);a[b.name+"_view"]=function(){a.renderMatrix.apply(b,arguments)};
var c=new Date,f=a.date.add(c,b.x_step,b.x_unit).valueOf()-c.valueOf();a["mouse_"+b.name]=function(c){var d=this._drag_event;if(this._drag_id)d=this.getEvent(this._drag_id),this._drag_event._dhx_changed=!0;c.x-=b.dx;for(var f=0,e=0,k=0;e<=this._cols.length-1;e++){var l=this._cols[e];f+=l;if(f>c.x){var i=(c.x-(f-l))/l,i=i<0?0:i;break}}for(f=0;k<this._colsS.heights.length;k++)if(f+=this._colsS.heights[k],f>c.y)break;c.fields={};b.y_unit[k]||(k=b.y_unit.length-1);if(k>=0&&b.y_unit[k]&&(c.section=c.fields[b.y_property]=
b.y_unit[k].key,d))d[b.y_property]=c.section;c.x=0;var m;if(e>=b._trace_x.length)m=a.date.add(b._trace_x[b._trace_x.length-1],b.x_step,b.x_unit);else{var n=b._trace_x[e+1]?b._trace_x[e+1]:a.date.add(b._trace_x[b._trace_x.length-1],b.x_step,b.x_unit),o=Math.ceil(i*(n-b._trace_x[e]));m=new Date(+b._trace_x[e]+o)}if(this._drag_mode=="move"&&this._drag_id&&this._drag_event){var d=this.getEvent(this._drag_id),p=this._drag_event;if(!p._move_delta)p._move_delta=(d.start_date-m)/6E4;m=a.date.add(m,p._move_delta,
"minute")}if(this._drag_mode=="resize"&&d)c.resize_from_start=!!(Math.abs(d.start_date-m)<Math.abs(d.end_date-m));c.y=Math.round((m-this._min_date)/(6E4*this.config.time_step));c.custom=!0;c.shift=this.config.time_step;return c}};a.render_timeline_event=function(b,d){var c=b[this.y_property],f=b._sorder,g=u(b,!1,this._step),h=u(b,!0,this._step),j=this.event_dy;this.event_dy=="full"&&(j=this.section_autoheight?this._section_height[c]-6:this.dy-3);this.resize_events&&(j=Math.max(Math.floor(j/b._count),
this.event_min_dy));var e=j-2;!b._inner&&this.event_dy=="full"&&(e=(e+2)*(b._count-f)-2);var k=2+f*j+(f?f*2:0);a.config.cascade_event_display&&(k=2+f*a.config.cascade_event_margin+(f?f*2:0));var l=j+k+2;if(!this._events_height[c]||this._events_height[c]<l)this._events_height[c]=l;var i=a.templates.event_class(b.start_date,b.end_date,b),i="dhx_cal_event_line "+(i||""),m=b.color?"background:"+b.color+";":"",n=b.textColor?"color:"+b.textColor+";":"",o=a.templates.event_bar_text(b.start_date,b.end_date,
b),p='<div event_id="'+b.id+'" class="'+i+'" style="'+m+""+n+"position:absolute; top:"+k+"px; height: "+e+"px; left:"+g+"px; width:"+Math.max(0,h-g)+"px;"+(b._text_style||"")+'">';if(a.config.drag_resize){var q="dhx_event_resize";p+="<div class='"+q+" "+q+"_start' style='height: "+e+"px;'></div><div class='"+q+" "+q+"_end' style='height: "+e+"px;'></div>"}p+=o+"</div>";if(d){var s=document.createElement("DIV");s.innerHTML=p;var v=this.order[c],t=a._els.dhx_cal_data[0].firstChild.rows[v].cells[1].firstChild;
a._rendered.push(s.firstChild);t.appendChild(s.firstChild)}else return p};a.renderMatrix=function(b,d){if(!d)a._els.dhx_cal_data[0].scrollTop=0;a._min_date=a.date[this.name+"_start"](a._date);a._max_date=a.date.add(a._min_date,this.x_size*this.x_step,this.x_unit);a._table_view=!0;if(this.second_scale){if(b&&!this._header_resized)this._header_resized=a.xy.scale_height,a.xy.scale_height*=2,a._els.dhx_cal_header[0].className+=" dhx_second_cal_header";if(!b&&this._header_resized){a.xy.scale_height/=2;
this._header_resized=!1;var c=a._els.dhx_cal_header[0];c.className=c.className.replace(/ dhx_second_cal_header/gi,"")}}J.call(this,b)};a._locate_cell_timeline=function(b){for(var b=b||event,d=b.target?b.target:b.srcElement,c={},f=a.matrix[a._mode],g=a.getActionData(b),h=0;h<f._trace_x.length-1;h++)if(+g.date<=f._trace_x[h+1])break;c.x=h;c.y=f.order[g.section];var j=a._isRender("cell")?1:0;c.src=f._scales[g.section].getElementsByTagName("td")[h+j];if(d.className.split(" ")[0]=="dhx_matrix_scell")c.x=
-1,c.src=d,c.scale=!0;return c};var M=a._click.dhx_cal_data;a._click.dhx_marked_timespan=a._click.dhx_cal_data=function(b){var d=M.apply(this,arguments),c=a.matrix[a._mode];if(c){var f=a._locate_cell_timeline(b);f&&(f.scale?a.callEvent("onYScaleClick",[f.y,c.y_unit[f.y],b||event]):a.callEvent("onCellClick",[f.x,f.y,c._trace_x[f.x],(c._matrix[f.y]||{})[f.x]||[],b||event]))}return d};a.dblclick_dhx_marked_timespan=a.dblclick_dhx_matrix_cell=function(b){var d=a.matrix[a._mode];if(d){var c=a._locate_cell_timeline(b);
c&&(c.scale?a.callEvent("onYScaleDblClick",[c.y,d.y_unit[c.y],b||event]):a.callEvent("onCellDblClick",[c.x,c.y,d._trace_x[c.x],(d._matrix[c.y]||{})[c.x]||[],b||event]))}};a.dblclick_dhx_matrix_scell=function(b){return a.dblclick_dhx_matrix_cell(b)};a._isRender=function(b){return a.matrix[a._mode]&&a.matrix[a._mode].render==b};a.attachEvent("onCellDblClick",function(b,d,c,f,g){if(!(this.config.readonly||g.type=="dblclick"&&!this.config.dblclick_create)){var h=a.matrix[a._mode],j={};j.start_date=h._trace_x[b];
j.end_date=h._trace_x[b+1]?h._trace_x[b+1]:a.date.add(h._trace_x[b],h.x_step,h.x_unit);j[a.matrix[a._mode].y_property]=h.y_unit[d].key;a.addEventNow(j,null,g)}});a.attachEvent("onBeforeDrag",function(){return!a._isRender("cell")});a.attachEvent("onEventChanged",function(a,d){d._timed=this.is_one_day_event(d)});var N=a._render_marked_timespan;a._render_marked_timespan=function(b,d,c){if(!a.config.display_marked_timespans)return[];if(a.matrix&&a.matrix[a._mode]){if(!a._isRender("cell")){var f=a.matrix[a._mode],
g=[],h=[],j=[];if(c)j=[d],h=[c];else{var e=f.order,k;for(k in e)e.hasOwnProperty(k)&&(h.push(k),j.push(f._scales[k]))}var l=a._min_date,i=a._max_date,m=[];if(b.days>6){var n=new Date(b.days);a.date.date_part(new Date(l))<=+n&&+i>=+n&&m.push(n)}else m.push.apply(m,a._get_dates_by_index(b.days));for(var o=b.zones,p=a._get_css_classes_by_config(b),q=0;q<h.length;q++)for(var d=j[q],c=h[q],s=0;s<m.length;s++)for(var v=m[s],t=0;t<o.length;t+=2){var w=o[t],A=o[t+1],y=new Date(+v+w*6E4),x=new Date(+v+A*6E4);
if(a._min_date<x&&a._max_date>y){var r=a._get_block_by_config(b);r.className=p;var z=u({start_date:y},!1,f._step)-1,B=u({start_date:x},!1,f._step)-1,C=B-z-1,D=f._section_height[c]-1;r.style.cssText="height: "+D+"px; left: "+z+"px; width: "+C+"px; top: 0;";d.insertBefore(r,d.firstChild);g.push(r)}}return g}}else return N.apply(a,[b,d,c])};var O=a._append_mark_now;a._append_mark_now=function(b){if(a.matrix&&a.matrix[a._mode]){var d=new Date,c=a._get_zone_minutes(d),f={days:+a.date.date_part(d),zones:[c,
c+1],css:"dhx_matrix_now_time",type:"dhx_now_time"};return a._render_marked_timespan(f)}else return O.apply(a,[b])};a.attachEvent("onViewChange",function(b,d){a.matrix&&a.matrix[d]&&a.markNow&&a.markNow()});a.attachEvent("onScaleAdd",function(b,d){var c=a._marked_timespans;if(c&&a.matrix&&a.matrix[a._mode])for(var f=a._mode,g=a._min_date,h=a._max_date,j=c.global,e=a.date.date_part(new Date(g));e<h;e=a.date.add(e,1,"day")){var k=+e,l=e.getDay(),i=[],m=j[k]||j[l];i.push.apply(i,a._get_configs_to_render(m));
if(c[f]&&c[f][d]){var n=a._get_types_to_render(c[f][d][l],c[f][d][k]);i.push.apply(i,a._get_configs_to_render(n))}for(var o=0;o<i.length;o++){var p=i[o],q=p.days;q<7?(q=k,a._render_marked_timespan(p,b,d),q=l):a._render_marked_timespan(p,b,d)}}})})()});