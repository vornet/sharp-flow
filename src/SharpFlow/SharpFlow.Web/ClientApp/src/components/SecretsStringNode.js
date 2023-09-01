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

function SecretsStringNode({ id, data }) {
  const [secret, setSecret] = React.useState();

  useEffect(() => {
    setSecret(data.state.Secret);
  }, [data]);

  return (
    <>
        <div className="custom-node__header">
            <strong>{id}{data.displayType?.length > 0 && ` : ${data.displayType}`}</strong>
        </div>
        <div>
          <label htmlFor="text">Secret:</label>
          <TextField 
            type="password"
            value={secret}
            onChange={(event) => {
              setSecret(event.target.value);
              console.log(data);
              data.update(event);
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

export default memo(SecretsStringNode);