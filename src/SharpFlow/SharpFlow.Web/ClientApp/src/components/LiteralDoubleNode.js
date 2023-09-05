import React, { memo, useEffect } from 'react';
import { Handle, Position, useReactFlow, useStoreApi } from 'reactflow';
import TextField from '@mui/material/TextField';

function Select({ handle, position }) { 
    return (
      <div className="custom-node__select">
        <div>{handle.id} : {handle.displayType} </div>
        <Handle type={handle.direction} position={position} id={handle.id} />
      </div>
    );
}

function LiteralDoubleNode({ id, data }) {
  const [number, setNumber] = React.useState(0.0);

  useEffect(() => {
    setNumber(data?.state?.Number ?? 0.0);
  }, [data]);

  return (
    <>
        <div className="custom-node__header">
            <strong>{id}{data.displayType?.length > 0 && ` : ${data.displayType}`}</strong>
        </div>
        <div>
          <TextField 
            label="Number"
            size="small"
            variant="standard"
            value={number}
            onChange={(e) => {
              const regex = /^[-+]?[0-9]*\.?[0-9]*$/;
              console.log(e.target.value)
              if (e.target.value === "" || regex.test(e.target.value)) {
                setNumber(e.target.value);
                data.update(id, e.target.value);
              }
            }}
          />
        </div>
        <div className="custom-node__body">
            <div className="custom-node__column-left">
                {data.handles.filter(handle => handle.direction === "target").map((handle) => (                    
                    <Select key={handle.id} nodeId={id} handle={handle} position={Position.Left} />
                ))}
            </div>
            <div className="custom-node__column-right">
            {data.handles.filter(handle => handle.direction === "source").map((handle) => (
                    <Select key={handle.id} nodeId={id} handle={handle} position={Position.Right} />
                ))}
            </div>
        </div>
    </>
  );
}

export default memo(LiteralDoubleNode);