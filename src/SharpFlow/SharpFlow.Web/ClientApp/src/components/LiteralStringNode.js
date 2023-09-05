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

function LiteralStringNode({ id, data }) {
  const [text, setText] = React.useState();

  useEffect(() => {
    setText(data?.state?.Text ?? "");
  }, [data]);

  return (
    <>
        <div className="custom-node__header">
            <strong>{id}{data.displayType?.length > 0 && ` : ${data.displayType}`}</strong>
        </div>
        <div>
          <TextField 
            label="Text"
            size="small"
            variant="standard"
            value={text}
            onChange={(event) => {
              setText(event.target.value);
              data.update(id, event.target.value);
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

export default memo(LiteralStringNode);