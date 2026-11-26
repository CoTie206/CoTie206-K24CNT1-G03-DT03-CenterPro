import React, { useEffect, useMemo, useState } from 'react';
import { createRoot } from 'react-dom/client';
import './style.css';

const dateValue = (date) => `${date.getFullYear()}-${String(date.getMonth()+1).padStart(2,'0')}-${String(date.getDate()).padStart(2,'0')}`;
const startOfWeek = () => { const d = new Date(); d.setHours(0,0,0,0); d.setDate(d.getDate() - ((d.getDay()+6)%7)); return d; };
const fmt = (value, options) => new Intl.DateTimeFormat('vi-VN', options).format(new Date(value));

function ScheduleBoard({ classesApi, schedulesApi }) {
  const [classes, setClasses] = useState([]);
  const [schedules, setSchedules] = useState([]);
  const [classId, setClassId] = useState('');
  const [from, setFrom] = useState(dateValue(startOfWeek()));
  const [to, setTo] = useState(() => { const d = startOfWeek(); d.setDate(d.getDate()+6); return dateValue(d); });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  const queryUrl = useMemo(() => {
    return schedulesApi;
  }, [schedulesApi]);
  const visibleSchedules = schedules.filter(s => {
    const day = dateValue(new Date(s.thoiGianBatDau));
    return (!classId || String(s.maLopHoc) === classId) && (!from || day >= from) && (!to || day <= to);
  });

  useEffect(() => {
    let cancelled = false;
    setLoading(true); setError('');
    Promise.all([
      fetch(classesApi, { credentials: 'same-origin', headers: { Accept: 'application/json' } }),
      fetch(queryUrl, { credentials: 'same-origin', headers: { Accept: 'application/json' } })
    ]).then(async ([classResponse, scheduleResponse]) => {
      if (!classResponse.ok || !scheduleResponse.ok) throw new Error('Không tải được dữ liệu. Hãy đăng nhập lại hoặc thử tải lại trang.');
      const [classData, scheduleData] = await Promise.all([classResponse.json(), scheduleResponse.json()]);
      if (!cancelled) { setClasses(classData); setSchedules(scheduleData); }
    }).catch((e) => { if (!cancelled) setError(e.message || 'Đã xảy ra lỗi khi tải dữ liệu.'); })
      .finally(() => { if (!cancelled) setLoading(false); });
    return () => { cancelled = true; };
  }, [classesApi, queryUrl]);

  return <div className="cp-react">
    <div className="cp-heading"><div><p className="cp-eyebrow">DỮ LIỆU TRỰC TIẾP</p><h2>Lớp học và lịch giảng dạy</h2><p>Thông tin được tải từ API CenterPro.</p></div><span className="cp-count">{classes.length} lớp</span></div>
    <div className="cp-toolbar">
      <label>Lớp học<select value={classId} onChange={e => setClassId(e.target.value)}><option value="">Tất cả lớp</option>{classes.map(c => <option key={c.maLopHoc} value={c.maLopHoc}>{c.tenLopHoc}</option>)}</select></label>
      <label>Từ ngày<input type="date" value={from} onChange={e => setFrom(e.target.value)} /></label>
      <label>Đến ngày<input type="date" value={to} onChange={e => setTo(e.target.value)} /></label>
      <button type="button" className="cp-refresh" onClick={() => setError('')}>Lọc lịch</button>
    </div>
    {error && <div className="cp-error" role="alert">{error}</div>}
    {loading ? <p className="cp-state">Đang tải dữ liệu…</p> : !error && visibleSchedules.length === 0 ? <p className="cp-state">Không có lịch trong khoảng thời gian này.</p> : !error && <div className="cp-table-wrap"><table><thead><tr><th>Thời gian</th><th>Lớp học</th><th>Giảng viên</th><th>Phòng</th></tr></thead><tbody>
      {visibleSchedules.map(s => <tr key={s.maLichHoc}><td><strong>{fmt(s.thoiGianBatDau,{weekday:'short',day:'2-digit',month:'2-digit'})}</strong><span>{fmt(s.thoiGianBatDau,{hour:'2-digit',minute:'2-digit'})} – {fmt(s.thoiGianKetThuc,{hour:'2-digit',minute:'2-digit'})}</span></td><td>{s.tenLopHoc}</td><td>{s.tenGiangVien}</td><td>{s.tenPhongHoc}</td></tr>)}
    </tbody></table></div>}
  </div>;
}

const mount = document.getElementById('centerpro-react-root');
if (mount) createRoot(mount).render(<ScheduleBoard classesApi={mount.dataset.classesApi} schedulesApi={mount.dataset.schedulesApi} />);
